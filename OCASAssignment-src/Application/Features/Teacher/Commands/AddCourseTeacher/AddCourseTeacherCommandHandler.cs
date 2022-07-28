using AutoMapper;
using MediatR;
using OCASAPI.Application.DTO.Common;
using OCASAPI.Application.Wrappers;

namespace OCASAPI.Application.Features
{
    public class AddCourseTeacherCommandHandler : IRequestHandler<AddCourseTeacherCommand,Response<bool>>
    {
        private readonly IMapper _mapper;
        private readonly ITeacherRepository _teacherRepository;

        public AddCourseTeacherCommandHandler(IMapper mapper, ITeacherRepository teacherRepository)
        {
            _mapper = mapper;
            _teacherRepository = teacherRepository;
        }

        public async Task<Response<bool>> Handle(AddCourseTeacherCommand request, CancellationToken cancellationToken)
        {
            var result = await _teacherRepository.AddCourseTeacherAsync(request.CourseId, request.TeacherId);

            return new Response<bool>(result);
        }
    }
}