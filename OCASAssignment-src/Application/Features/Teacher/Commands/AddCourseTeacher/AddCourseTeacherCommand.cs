using MediatR;
using OCASAPI.Application.DTO.Common;
using OCASAPI.Application.Wrappers;

namespace OCASAPI.Application.Features
{
    public class AddCourseTeacherCommand : IRequest<Response<bool>>
    {
        public AddCourseTeacherCommand(Guid courseId, Guid teacherId)
        {
            CourseId  = courseId;
            TeacherId = teacherId;
        }

        public Guid CourseId { get; }
        public Guid TeacherId { get; }
    }
}