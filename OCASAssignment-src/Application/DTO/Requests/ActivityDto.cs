using System.ComponentModel.DataAnnotations;

namespace OCASAPI.Application.Requests
{
    public class ActivityDto
    {
        public Guid Id {get; set;}
        [Required]
        [MaxLength(100)]
        public string ActivityName {get; set;}
        [Required]
        [MaxLength(200)]
        public string Description {get; set;}
    }
}




