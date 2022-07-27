using MediatR;
using OCASAPI.Application.DTO.Common;
using OCASAPI.Application.Wrappers;

namespace OCASAPI.Application.Features
{
    public class GetSchoolTeachersCommand : IRequest<Response<IReadOnlyList<TeacherDto>>>
    {
        public GetSchoolTeachersCommand(Guid schoolId)
        {
            SchoolId = schoolId;
        }

        public Guid SchoolId { get; }
    }
}