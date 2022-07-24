namespace OCAS.Domain.Common
{
    public class Student : BaseEntity
    {
        public string FirstName {get; set;}
        public string LastName {get; set;}
        public int Age {get; set;}
        public IList<Course> Courses {get; set;}
        public IList<Grade> Grades {get; set;}
    }
}
