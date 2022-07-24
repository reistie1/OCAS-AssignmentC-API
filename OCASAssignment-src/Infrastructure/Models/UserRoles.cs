using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace OCASAPI.Infrastructure.Models
{
   [Table("AspNetUserRoles")]
    public class UserRoles : IdentityUserRole<Guid>{}
}