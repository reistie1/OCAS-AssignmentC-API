using MediatR;
using OCASAPI.Application.DTO.Common;
using OCASAPI.Application.Wrappers;

namespace OCASAPI.Application.Features
{
    public class GetSchoolStudentsCommand : IRequest<Response<IReadOnlyList<StudentDto>>>
    {
        public GetSchoolStudentsCommand(Guid schoolId)
        {
            SchoolId = schoolId;
        }

        public Guid SchoolId{ get; }
    }
}