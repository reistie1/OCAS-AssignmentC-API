using MediatR;
using OCASAPI.Application.DTO.Common;
using OCASAPI.Application.Wrappers;

namespace OCASAPI.Application.Features
{
    public class GetStudentGradesCommand : IRequest<Response<IReadOnlyList<GradeDto>>>
    {
        public GetStudentGradesCommand(Guid studentId)
        {
            StudentId = studentId;
        }

        public Guid StudentId { get; }
    }
}