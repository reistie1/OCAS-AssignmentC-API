
namespace OCASAPI.Application.DTO.Requests
{
    public class RemoveStudentGradeRequest
    {
        public Guid StudentId {get; set;}
        public Guid CourseId {get; set;}
    }
}