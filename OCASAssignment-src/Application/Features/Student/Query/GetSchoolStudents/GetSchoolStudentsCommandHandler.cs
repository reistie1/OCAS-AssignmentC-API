using AutoMapper;
using MediatR;
using OCAS.Domain.Common;
using OCASAPI.Application.DTO.Common;
using OCASAPI.Application.Wrappers;

namespace OCASAPI.Application.Features
{
    public class GetSchoolStudentsCommandHandler : IRequestHandler<GetSchoolStudentsCommand,Response<IReadOnlyList<StudentDto>>>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public  GetSchoolStudentsCommandHandler(IMapper mapper, IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        public async Task<Response<IReadOnlyList<StudentDto>>> Handle(GetSchoolStudentsCommand request, CancellationToken cancellationToken)
        {
            var result = await _studentRepository.GetSchoolStudentsAsync(s => s.SchoolId == request.SchoolId);

            return new Response<IReadOnlyList<StudentDto>>(_mapper.Map<IReadOnlyList<StudentDto>>(result));
        }
    }
}