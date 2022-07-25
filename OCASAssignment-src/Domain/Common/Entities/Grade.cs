using System.ComponentModel.DataAnnotations;

namespace OCAS.Domain.Common
{
    public class Grade : BaseEntity
    {
        [Required]
        public Guid CourseId {get; set;}
        public virtual Course Course {get; set;}
        [Required]
        public Guid StudentId {get; set;}
        public virtual Student Student {get; set;}
        public int NumericGrade {get; set;}
        public char AlphabeticGrade {get; set;}
    }
}
