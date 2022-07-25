namespace OCASAPI.Application.DTO.Common
{
    public class StudentDto
    {
        public Guid Id {get; set;}
        public string FirstName {get; set;}
        public string LastName {get; set;}
        public int Age {get; set;}
        public IList<CourseDto> Courses {get; set;}
        //public IList<Grade> Grades {get; set;}
    }
}