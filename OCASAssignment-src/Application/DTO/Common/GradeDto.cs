namespace OCASAPI.Application.DTO.Common
{
    public class GradeDto
    {
        public Guid Id {get; set;}
        public Guid CourseId {get; set;}
        public CourseDto Course {get; set;}
        public Guid StudentId {get; set;}
        public StudentDto Student {get; set;}
        public int NumericGrade {get; set;}
        public char AlphabeticGrade {get; set;}
    }
}