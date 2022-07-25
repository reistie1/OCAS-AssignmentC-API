using AutoMapper;
using MediatR;
using OCASAPI.Application.DTO.Requests;
using OCASAPI.Application.Wrappers;

namespace OCASAPI.Application.Features
{
    public class GetSchoolInformationCommandHandler : IRequestHandler<GetSchoolInformationCommand,Response<SchoolInfoRequest>>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolRepository _schoolRepository;

        public GetSchoolInformationCommandHandler(IMapper mapper, ISchoolRepository schoolRepository)
        {
            _mapper = mapper;
            _schoolRepository = schoolRepository;
        }


        public async Task<Response<SchoolInfoRequest>> Handle(GetSchoolInformationCommand request, CancellationToken cancellationToken)
        {
            var result = await _schoolRepository.GetSchoolInformationAsync(request.SchoolId);

            return new Response<SchoolInfoRequest>(_mapper.Map<SchoolInfoRequest>(result));
        }
    }
}