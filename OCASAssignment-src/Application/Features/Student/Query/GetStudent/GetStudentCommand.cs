using MediatR;
using OCASAPI.Application.DTO.Common;
using OCASAPI.Application.Wrappers;

namespace OCASAPI.Application.Features
{
    public class GetStudentCommand : IRequest<Response<StudentDto>>
    {
        public GetStudentCommand(Guid studentId)
        {
            StudentId = studentId;
        }

        public Guid StudentId { get; }
    }
}