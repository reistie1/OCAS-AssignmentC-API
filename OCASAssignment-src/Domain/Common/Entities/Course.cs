using System.ComponentModel.DataAnnotations;

namespace OCAS.Domain.Common
{
    public class Course : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string CourseName {get; set;}
        [Required]
        [MaxLength(10)]
        public string CourseCode {get; set;}
        [Required]
        [MaxLength(255)]
        public string Description {get; set;}
        public Guid? TeacherId {get; set;}
        public virtual Teacher Teacher {get; set;}
        [Required]
        public Guid SchoolId {get; set;}
        public virtual School School {get; set;}
        public IList<Student> Students {get; set;}
        public IList<Grade> Grades {get; set;}

    }
}
