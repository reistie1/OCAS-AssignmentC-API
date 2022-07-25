using AutoMapper;
using MediatR;
using OCAS.Domain.Common;
using OCASAPI.Application.DTO.Common;
using OCASAPI.Application.Wrappers;

namespace OCASAPI.Application.Features
{
    public class AddStudentGradeCommandHandler : IRequestHandler<AddStudentGradeCommand,Response<GradeDto>>
    {
        private readonly IMapper _mapper;
        private readonly IGradeRepository _gradeRepository;

        public AddStudentGradeCommandHandler(IMapper mapper, IGradeRepository gradeRepository)
        {
            _mapper = mapper;
            _gradeRepository = gradeRepository;
        }

        public async Task<Response<GradeDto>> Handle(AddStudentGradeCommand request, CancellationToken cancellationToken)
        {
            var result = await _gradeRepository.AddStudentGradeAsync(request.StudentId, _mapper.Map<Grade>(request.Grade));

            return new Response<GradeDto>(_mapper.Map<GradeDto>(result));
        }
    }
}