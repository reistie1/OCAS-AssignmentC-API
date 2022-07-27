namespace OCAS.Domain.Common
{
    public class School : BaseEntity
    {
        public string Name {get; set;}
        public Guid AddressId {get; set;}
        public virtual Address Address {get; set;}
        public IList<Student> Students {get; set;}
        public IList<Course> Courses {get; set;}
        public IList<Teacher> Teachers {get; set;}
    }
}
