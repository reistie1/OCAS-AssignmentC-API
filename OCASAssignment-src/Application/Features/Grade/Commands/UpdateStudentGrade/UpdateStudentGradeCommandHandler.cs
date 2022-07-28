using AutoMapper;
using MediatR;
using OCAS.Domain.Common;
using OCASAPI.Application.DTO.Common;
using OCASAPI.Application.Wrappers;

namespace OCASAPI.Application.Features
{
    public class UpdateStudentGradeCommandHandler : IRequestHandler<UpdateStudentGradeCommand,Response<GradeDto>>
    {
        private readonly IMapper _mapper;
        private readonly IGradeRepository _gradeRepository;

        public UpdateStudentGradeCommandHandler(IMapper mapper, IGradeRepository gradeRepository)
        {
            _mapper = mapper;
            _gradeRepository = gradeRepository;
        }

        public async Task<Response<GradeDto>> Handle(UpdateStudentGradeCommand request, CancellationToken cancellationToken)
        {
            var result = await _gradeRepository.UpdateStudentGradeAsync(_mapper.Map<Grade>(request.Grade));

            return new Response<GradeDto>(_mapper.Map<GradeDto>(result));
        }
    }
}