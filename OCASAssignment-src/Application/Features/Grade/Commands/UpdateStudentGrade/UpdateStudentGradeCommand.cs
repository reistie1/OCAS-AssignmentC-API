using MediatR;
using OCASAPI.Application.DTO.Common;
using OCASAPI.Application.Wrappers;

namespace OCASAPI.Application.Features
{
    public class UpdateStudentGradeCommand : IRequest<Response<GradeDto>>
    {
        public UpdateStudentGradeCommand(Guid studentId, GradeDto grade)
        {
            StudentId = studentId;
            Grade = grade;
        }

        public Guid StudentId { get; }
        public GradeDto Grade { get; }
    }
}