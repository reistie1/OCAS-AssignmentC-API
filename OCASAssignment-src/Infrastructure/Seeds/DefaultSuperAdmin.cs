
using Microsoft.AspNetCore.Identity;
using OCASAPI.Infrastructure.Context;
using OCASAPI.Infrastructure.Enums;
using OCASAPI.Infrastructure.Models;


namespace OCASAPI.Infrastructure.Identity.Seeds
{
    /// <summary>
    /// Seed the default system administrator for the ocas systems
    /// </summary>
    public static class DefaultSuperAdmin
    {
        public static async Task SeedAsync(UserManager<User> userManager, RoleManager<Role> roleManager, ApplicationContext context)
        {
            var adminPassword = "";

            if(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT").ToLower() == "development")
            {
                adminPassword = Environment.GetEnvironmentVariable("DefaultPass");
            }

            //Seed Default User
            var defaultUser = new User
            {
                Id = Guid.NewGuid(),
                FirstName = "System",
                LastName = "Admin",
                PhoneNumber = "519-497-0021",
                UserName = "josh.reist@gmail.com",
                NormalizedUserName = "josh.reist@gmail.com".ToUpper(), 
                EmailConfirmed = true,
                Role = RolesTypes.Sys_Admin.ToString(),
                SchoolId = Guid.NewGuid(),
                Email = "josh.reist@gmail.com",
                NormalizedEmail = "josh.reist@gmail.com".ToUpper()
            };

            var hasher = new PasswordHasher<User>();
            defaultUser.PasswordHash = hasher.HashPassword(defaultUser, adminPassword);

            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {                     
                    await userManager.CreateAsync(defaultUser);
                    await userManager.AddToRoleAsync(defaultUser, RolesTypes.Student.ToString());
                    await userManager.AddToRoleAsync(defaultUser, RolesTypes.Teacher.ToString());
                    await userManager.AddToRoleAsync(defaultUser, RolesTypes.Admin.ToString());
                    await userManager.AddToRoleAsync(defaultUser, RolesTypes.Sys_Admin.ToString());
                }
            }
        }
    }
}