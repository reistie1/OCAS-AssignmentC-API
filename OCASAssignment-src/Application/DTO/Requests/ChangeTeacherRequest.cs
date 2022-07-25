
namespace OCASAPI.Application.DTO.Requests
{
    public class ChangeTeacherRequest
    {
        public Guid OldTeacherId {get; set;}
        public Guid NewTeacherId {get; set;}
        public Guid CourseId {get; set;}
    }
}