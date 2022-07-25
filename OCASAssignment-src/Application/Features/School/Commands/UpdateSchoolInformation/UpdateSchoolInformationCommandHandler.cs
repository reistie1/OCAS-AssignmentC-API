using AutoMapper;
using MediatR;
using OCAS.Domain.Common;
using OCASAPI.Application.DTO.Requests;
using OCASAPI.Application.Wrappers;

namespace OCASAPI.Application.Features
{
    public class UpdateSchoolInformationCommandHandler : IRequestHandler<UpdateSchoolInformationCommand,Response<SchoolInfoRequest>>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolRepository _schoolRepository;

        public UpdateSchoolInformationCommandHandler(IMapper mapper, ISchoolRepository schoolRepository)
        {
            _mapper = mapper;
            _schoolRepository = schoolRepository;
        }


        public async Task<Response<SchoolInfoRequest>> Handle(UpdateSchoolInformationCommand request, CancellationToken cancellationToken)
        {
            var result = await _schoolRepository.UpdateSchoolInformationAsync(_mapper.Map<School>(request.School));

            return new Response<SchoolInfoRequest>(_mapper.Map<SchoolInfoRequest>(result));
        }
    }
}