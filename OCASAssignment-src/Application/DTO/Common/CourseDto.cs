namespace OCASAPI.Application.DTO.Common
{
    public class CourseDto
    {
        public Guid? Id {get; set;}
        public string CourseName {get; set;}
        public string CourseCode {get; set;}
        public string Description {get; set;}
        public Guid SchoolId {get; set;}

    }
}