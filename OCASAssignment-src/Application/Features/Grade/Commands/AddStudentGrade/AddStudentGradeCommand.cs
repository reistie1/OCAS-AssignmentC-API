using MediatR;
using OCASAPI.Application.DTO.Common;
using OCASAPI.Application.Wrappers;

namespace OCASAPI.Application.Features
{
    public class AddStudentGradeCommand : IRequest<Response<GradeDto>>
    {
        public AddStudentGradeCommand(Guid studentId, GradeDto grade)
        {
            StudentId = studentId;
            Grade = grade;
        }

        public Guid StudentId { get; }
        public GradeDto Grade { get; }
    }
}