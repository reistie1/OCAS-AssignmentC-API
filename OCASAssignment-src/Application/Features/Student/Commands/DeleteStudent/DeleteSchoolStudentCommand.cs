using MediatR;
using OCASAPI.Application.Wrappers;

namespace OCASAPI.Application.Features
{
    public class DeleteSchoolStudentCourseCommand : IRequest<Response<bool>>
    {
        public DeleteSchoolStudentCourseCommand(Guid studentId)
        {
            StudentId = studentId;
        }

        public Guid StudentId { get; }
    }
}