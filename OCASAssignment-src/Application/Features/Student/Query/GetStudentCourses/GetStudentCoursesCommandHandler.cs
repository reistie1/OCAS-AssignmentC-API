using AutoMapper;
using MediatR;
using OCASAPI.Application.DTO.Common;
using OCASAPI.Application.Wrappers;

namespace OCASAPI.Application.Features
{
    public class GetStudentCourseCommandHandler : IRequestHandler<GetStudentCoursesCommand,Response<IReadOnlyList<StudentDto>>>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public GetStudentCourseCommandHandler(IMapper mapper, IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        public async Task<Response<IReadOnlyList<StudentDto>>> Handle(GetStudentCoursesCommand request, CancellationToken cancellationToken)
        {
            var result = await _studentRepository.GetStudentCoursesAsync(s => s.Id == request.StudentId);

            return new Response<IReadOnlyList<StudentDto>>(_mapper.Map<IReadOnlyList<StudentDto>>(result));
        }
    }
}