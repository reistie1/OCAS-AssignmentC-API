using System.ComponentModel.DataAnnotations;

namespace OCAS.Domain.Common
{
    public class ActivitySignUp : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string FirstName {get; set;}
        [Required]
        [MaxLength(100)]
        public string LastName {get; set;}
        [Required]
        public string Email {get; set;}
        [MaxLength(500)]
        public string Comments {get; set;}
        [Required]
        public Guid ActivityId {get; set;}
        public virtual Activity Activity {get; set;}
        public DateTime SignedUpDate {get; set;}
        public string Gender {get; set;}

    }
}
