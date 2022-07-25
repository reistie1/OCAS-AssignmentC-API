using MediatR;
using OCASAPI.Application.DTO.Requests;
using OCASAPI.Application.Wrappers;

namespace OCASAPI.Application.Features
{
    public class GetSchoolInformationCommand : IRequest<Response<SchoolInfoRequest>>
    {
        public GetSchoolInformationCommand(Guid schoolId)
        {
            SchoolId = schoolId;
        }

        public Guid SchoolId { get; }
    }
}