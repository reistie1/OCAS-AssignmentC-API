using MediatR;
using OCASAPI.Application.Wrappers;

namespace OCASAPI.Application.Features
{
    public class DeleteSchoolStudentCommand : IRequest<Response<bool>>
    {
        public DeleteSchoolStudentCommand(Guid studentId)
        {
            StudentId = studentId;
        }

        public Guid StudentId { get; }
    }
}