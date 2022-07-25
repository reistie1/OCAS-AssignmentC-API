using AutoMapper;
using MediatR;
using OCASAPI.Application.DTO.Common;
using OCASAPI.Application.Wrappers;

namespace OCASAPI.Application.Features
{
    public class GetStudentGradesCommandHandler : IRequestHandler<GetStudentGradesCommand,Response<IReadOnlyList<GradeDto>>>
    {
        private readonly IMapper _mapper;
        private readonly IGradeRepository _gradeRepository;

        public GetStudentGradesCommandHandler(IMapper mapper, IGradeRepository gradeRepository)
        {
            _mapper = mapper;
            _gradeRepository = gradeRepository;
        }

        public async Task<Response<IReadOnlyList<GradeDto>>> Handle(GetStudentGradesCommand request, CancellationToken cancellationToken)
        {
            var result = await _gradeRepository.GetStudentGradesAsync(request.StudentId);

            return new Response<IReadOnlyList<GradeDto>>(_mapper.Map<IReadOnlyList<GradeDto>>(result));
        }
    }
}