using MediatR;
using OCASAPI.Application.DTO.Common;
using OCASAPI.Application.Wrappers;

namespace OCASAPI.Application.Features
{
    public class AddStudentCourseCommand : IRequest<Response<bool>>
    {
        public AddStudentCourseCommand(Guid studentId, Guid courseId)
        {
            StudentId = studentId;
            CourseId = courseId;
        }

        public Guid StudentId { get; }
        public Guid CourseId { get; }
    }
}