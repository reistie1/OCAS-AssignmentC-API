using Microsoft.AspNetCore.Mvc;
using OCASAPI.Application.DTO.Requests;
using OCASAPI.Application.Interfaces;

namespace OCASAPI.WebAPI.Controllers
{
    [ApiController]
    public class AccountController : BaseAPIController
    {
        private readonly IAccountService _accountService;
        private readonly IAppLogger<AccountController> _logger;

        public AccountController(IAccountService accountService, IAppLogger<AccountController> logger)
        {
            _accountService = accountService;
            _logger = logger;
        }

        [HttpPost("/register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequest request)
        {
            try
            {
                var result = await _accountService.Register(request);

                return Ok(result);
            }
            catch(Exception e)
            {
                _logger.LogWarning("Error registering school {error} - {stackTrace}", e.Message, e.StackTrace);
                return BadRequest(e);
            }
            
        }

        [HttpPost("/register-user")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserRequest request)
        {
            try
            {
                var result = await _accountService.RegisterUser(request);

                return Ok(result);
            }
            catch(Exception e)
            {
                _logger.LogWarning("Error registering user {error} - {stackTrace}", e.Message, e.StackTrace);
                return BadRequest(e);
            }
            
        }

        [HttpPost("/login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            try
            {
                var result = await _accountService.Login(request);

                return Ok(result);
            }
            catch(Exception e)
            {
                _logger.LogWarning("Error logging in user {error} - {stackTrace}", e.Message, e.StackTrace);
                return BadRequest(e);
            }
        }


    }
}