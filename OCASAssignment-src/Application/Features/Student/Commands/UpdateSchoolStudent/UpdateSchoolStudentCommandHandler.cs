using AutoMapper;
using MediatR;
using OCAS.Domain.Common;
using OCASAPI.Application.DTO.Common;
using OCASAPI.Application.Wrappers;

namespace OCASAPI.Application.Features
{
    public class UpdateSchoolStudentCommandHandler : IRequestHandler<UpdateSchoolStudentCommand,Response<StudentDto>>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public UpdateSchoolStudentCommandHandler(IMapper mapper, IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        public async Task<Response<StudentDto>> Handle(UpdateSchoolStudentCommand request, CancellationToken cancellationToken)
        {
            var result = await _studentRepository.UpdateStudentAsync(_mapper.Map<Student>(request.Student));

            return new Response<StudentDto>(_mapper.Map<StudentDto>(result));
        }
    }
}