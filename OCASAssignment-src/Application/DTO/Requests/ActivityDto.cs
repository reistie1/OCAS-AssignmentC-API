using System.ComponentModel.DataAnnotations;

namespace OCASAPI.Application.Requests
{
    public class ActivityDto
    {
        public Guid Id {get; set;}
        [Required]
        [MaxLength(150)]
        public string ActivityName {get; set;}
        [Required]
        [MaxLength(500)]
        public string Description {get; set;}
    }
}