using Microsoft.AspNetCore.Identity;
using OCASAPI.Infrastructure.Enums;
using OCASAPI.Infrastructure.Models;

namespace OCASAPI.Infrastructure.Identity.Seeds
{
    /// <summary>
    /// Seed all the default roles used to restrict users access to certain resources
    /// </summary>
    public static class DefaultRoles
    {
        public static async Task SeedAsync(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            //Seed Roles
            await roleManager.CreateAsync(new Role{Name = RolesTypes.Sys_Admin.ToString(), Id = Guid.NewGuid(), NormalizedName = RolesTypes.Sys_Admin.ToString().ToUpper()});
            await roleManager.CreateAsync(new Role{Name = RolesTypes.Admin.ToString(), Id = Guid.NewGuid(), NormalizedName = RolesTypes.Admin.ToString().ToUpper()});
            await roleManager.CreateAsync(new Role{Name = RolesTypes.Teacher.ToString(), Id = Guid.NewGuid(), NormalizedName = RolesTypes.Teacher.ToString().ToUpper()});
            await roleManager.CreateAsync(new Role{Name = RolesTypes.Student.ToString(), Id = Guid.NewGuid(), NormalizedName = RolesTypes.Student.ToString().ToUpper()});
        }
    }
}