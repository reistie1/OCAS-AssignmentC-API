using MediatR;
using OCASAPI.Application.DTO.Requests;
using OCASAPI.Application.Wrappers;

namespace OCASAPI.Application.Features
{
    public class UpdateSchoolInformationCommand : IRequest<Response<SchoolInfoRequest>>
    {
        public UpdateSchoolInformationCommand(SchoolInfoRequest school)
        {
            School = school;
        }

        public SchoolInfoRequest School { get; }
    }
}