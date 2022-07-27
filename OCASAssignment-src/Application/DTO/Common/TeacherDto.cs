namespace OCASAPI.Application.DTO.Common
{
    public class TeacherDto
    {
        public Guid Id {get; set;}
        public string FirstName {get; set;}
        public string LastName {get; set;}
        public string SubjectClassifier {get; set;}
        public Guid SchoolId {get; set;}
    }
}