using Microsoft.AspNetCore.Identity;

namespace OCASAPI.Infrastructure.Models
{
    public class User : IdentityUser<Guid> 
    {
        public string FirstName {get; set;}
        public string LastName {get; set;}
        public Guid RoleId {get; set;}
        public string Role {get; set;}
        public Guid SchoolId {get; set;}

    }
}