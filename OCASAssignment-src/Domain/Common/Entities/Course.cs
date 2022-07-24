namespace OCAS.Domain.Common
{
    public class Course : BaseEntity
    {
        public string CourseName {get; set;}
        public string CourseCode {get; set;}
        public string Description {get; set;}
        public IList<Student> Students {get; set;}
        public IList<Grade> Grades {get; set;}

    }
}
