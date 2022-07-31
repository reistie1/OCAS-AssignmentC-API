using System.ComponentModel.DataAnnotations;

namespace OCAS.Domain.Common
{
    public class Activity : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string ActivityName {get; set;}
        [Required]
        [MaxLength(200)]
        public string Description {get; set;}
        public List<ActivitySignUp> SignedUp {get; set;}

    }
}
