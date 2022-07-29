using System.ComponentModel.DataAnnotations;

namespace OCAS.Domain.Common
{
    public class Activity : BaseEntity
    {
        [Required]
        [MaxLength(150)]
        public string ActivityName {get; set;}
        [Required]
        [MaxLength(500)]
        public string Description {get; set;}
        public List<ActivitySignUp> SignedUp {get; set;}

    }
}
