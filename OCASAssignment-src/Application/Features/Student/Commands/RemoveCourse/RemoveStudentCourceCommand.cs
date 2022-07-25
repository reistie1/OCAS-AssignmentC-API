using MediatR;
using OCASAPI.Application.Wrappers;

namespace OCASAPI.Application.Features
{
    public class RemoveStudentCourseCommand : IRequest<Response<bool>>
    {
        public RemoveStudentCourseCommand(Guid studentId, Guid courseId)
        {
            StudentId = studentId;
            CourseId = courseId;
        }

        public Guid StudentId { get; }
        public Guid CourseId { get; }
    }
}