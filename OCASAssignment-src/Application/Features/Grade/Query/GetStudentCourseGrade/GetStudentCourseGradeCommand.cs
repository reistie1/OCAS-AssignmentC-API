using MediatR;
using OCASAPI.Application.DTO.Common;
using OCASAPI.Application.Wrappers;

namespace OCASAPI.Application.Features
{
    public class GetStudentCourseGradeCommand : IRequest<Response<GradeDto>>
    {
        public GetStudentCourseGradeCommand(Guid studentId, Guid courseId)
        {
            StudentId = studentId;
            CourseId = courseId;
        }

        public Guid StudentId { get; }
        public Guid CourseId { get; }
    }
}