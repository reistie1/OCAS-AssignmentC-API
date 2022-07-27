using MediatR;
using OCASAPI.Application.DTO.Common;
using OCASAPI.Application.Wrappers;

namespace OCASAPI.Application.Features
{
    public class DeleteSchoolTeacherCommand : IRequest<Response<bool>>
    {
        public DeleteSchoolTeacherCommand(Guid teacherId)
        {
            TeacherId = teacherId;
        }

        public Guid TeacherId { get; }
    }
}