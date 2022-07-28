using AutoMapper;
using MediatR;
using OCASAPI.Application.DTO.Common;
using OCASAPI.Application.Wrappers;

namespace OCASAPI.Application.Features
{
    public class GetStudentCommandHandler : IRequestHandler<GetStudentCommand,Response<StudentDto>>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public GetStudentCommandHandler(IMapper mapper, IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        public async Task<Response<StudentDto>> Handle(GetStudentCommand request, CancellationToken cancellationToken)
        {
            var result = await _studentRepository.GetStudentAsync(request.StudentId);

            return new Response<StudentDto>(_mapper.Map<StudentDto>(result));
        }
    }
}