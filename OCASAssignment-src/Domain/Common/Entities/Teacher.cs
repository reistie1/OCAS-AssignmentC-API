namespace OCAS.Domain.Common
{
    public class Teacher : BaseEntity
    {
        public string FirstName {get; set;}
        public string LastName {get; set;}
        public int SubjectClassifier {get; set;}
        public IList<Course> Courses {get; set;}
        public Guid SchoolId {get; set;}
        public virtual School School {get; set;}
    }
}
