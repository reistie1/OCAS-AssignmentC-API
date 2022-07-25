using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using OCAS.Domain.Common;
using OCASAPI.Application.DTO.Requests;
using OCASAPI.Application.DTO.Responses;
using OCASAPI.Application.Exceptions;
using OCASAPI.Application.Wrappers;
using OCASAPI.Infrastructure.Context;
using OCASAPI.Infrastructure.Models;

namespace OCASAPI.Infrastructure.Services
{
    public class AccountService : IAccountService
    {
        private readonly IdentityContext _context;
        private readonly ApplicationContext _appcontext;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly SignInManager<User> _signInManager;

        public AccountService(IdentityContext context, ApplicationContext appcontext,   
            UserManager<User> userManager, 
            RoleManager<Role> roleManager, 
            SignInManager<User> signInManager)
        {
            _context = context;
            _appcontext = appcontext;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;    
        }

        public async Task<AuthenticationResponse> Login(LoginRequest request)
        {
            User user = await _userManager.FindByEmailAsync(request.Email);
                        
            if(user == null)
            {
                throw new ApiExceptions($"No accounts with {request.Email} were found.");
            }

            if(user.LockoutEnd != null) 
            {
                if(!await _userManager.GetLockoutEnabledAsync(user))
                {
                    await _userManager.SetLockoutEnabledAsync(user, true);
                } 
                throw new ApiExceptions($"Account with email address: {request.Email} is locked out");
            }

            SignInResult result = await _signInManager.PasswordSignInAsync(user, request.Password, false, lockoutOnFailure: true);
            
            if(result.IsLockedOut)
            {
                throw new ApiExceptions($"{request.Email} is locked out");
            }
            if(!result.Succeeded) 
            {
                throw new ApiExceptions($"Invalid Credentials for '{request.Email}'.");
            }

            IList<string> rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
    
            return new AuthenticationResponse(){Email = user.Email, Role = user.Role}; 
        }

        public async Task<Response<bool>> Register(RegistrationRequest request)
        {
            var address = new Address()
            {
                Address1 = request.Address.Address1,
                Address2 = request.Address.Address2,
                City = request.Address.City,
                PostalCode = request.Address.PostalCode,
                Province = request.Address.Province
            };

            var registration = new School()
            {
                Name = request.Name,
                Address = address
            };

            await _appcontext.AddAsync(registration);

            var result = await _appcontext.SaveChangesAsync();

            if(result == 0)
            {
                throw new ApiExceptions("Error registering.");
            }
            else
            {
                return new Response<bool>(true);
            }
        }

        public async Task<Response<bool>> RegisterUser(RegisterUserRequest request)
        {
            var newUser = new User()
            {
                Id = Guid.NewGuid(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                Role = request.Role
            };

            IdentityResult userResult = await _userManager.CreateAsync(newUser, request.DefaultPassword);
            IdentityResult roleResult = await _userManager.AddToRoleAsync(newUser, request.Role);

            if(!userResult.Succeeded || !roleResult.Succeeded)
            {
                throw new ApiExceptions($"Error registering user.");
            }
            else
            {
                IdentityResult result = await _userManager.AddClaimAsync(newUser, new Claim(ClaimTypes.Role, request.Role));

                return new Response<bool>(true);
            }
        }
    }
}