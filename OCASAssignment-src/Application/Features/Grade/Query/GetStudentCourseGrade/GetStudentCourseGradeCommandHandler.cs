using AutoMapper;
using MediatR;
using OCASAPI.Application.DTO.Common;
using OCASAPI.Application.Wrappers;

namespace OCASAPI.Application.Features
{
    public class GetStudentCourseGradeCommandHandler : IRequestHandler<GetStudentCourseGradeCommand,Response<GradeDto>>
    {
        private readonly IMapper _mapper;
        private readonly IGradeRepository _gradeRepository;

        public GetStudentCourseGradeCommandHandler(IMapper mapper, IGradeRepository gradeRepository)
        {
            _mapper = mapper;
            _gradeRepository = gradeRepository;
        }

        public async Task<Response<GradeDto>> Handle(GetStudentCourseGradeCommand request, CancellationToken cancellationToken)
        {
            var result = await _gradeRepository.GetStudentCourseGradeAsync(request.StudentId, request.CourseId);

            return new Response<GradeDto>(_mapper.Map<GradeDto>(result));
        }
    }
}