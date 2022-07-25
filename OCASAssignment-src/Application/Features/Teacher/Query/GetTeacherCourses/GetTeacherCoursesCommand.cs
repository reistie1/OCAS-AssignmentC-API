using MediatR;
using OCASAPI.Application.DTO.Common;
using OCASAPI.Application.DTO.Requests;
using OCASAPI.Application.Wrappers;

namespace OCASAPI.Application.Features
{
    public class GetTeacherCoursesCommand : IRequest<Response<IReadOnlyList<TeacherDto>>>
    {
        public GetTeacherCoursesCommand(Guid teacherId)
        {
            TeacherId = teacherId;
        }

        public Guid TeacherId { get; }
    }
}