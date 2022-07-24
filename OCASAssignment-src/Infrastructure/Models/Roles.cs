using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace OCASAPI.Infrastructure.Models
{
    [Table("AspNetRoles")]
    public class Roles : IdentityRole<Guid>{}
}