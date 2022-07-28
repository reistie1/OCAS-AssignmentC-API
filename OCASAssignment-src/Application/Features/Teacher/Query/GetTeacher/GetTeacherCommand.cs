using MediatR;
using OCASAPI.Application.DTO.Common;
using OCASAPI.Application.Wrappers;

namespace OCASAPI.Application.Features
{
    public class GetTeacherCommand : IRequest<Response<TeacherDto>>
    {
        public GetTeacherCommand(Guid teacherId)
        {
            TeacherId = teacherId;
        }

        public Guid TeacherId { get; }
    }
}