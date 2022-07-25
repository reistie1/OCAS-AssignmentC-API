using MediatR;
using OCASAPI.Application.DTO.Common;
using OCASAPI.Application.Wrappers;

namespace OCASAPI.Application.Features
{
    public class RemoveCourseTeacherCommandHandler : IRequestHandler<RemoveCourseTeacherCommand,Response<bool>>
    {
        private readonly ITeacherRepository _teacherRepository;

        public RemoveCourseTeacherCommandHandler(ITeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }

        public async Task<Response<bool>> Handle(RemoveCourseTeacherCommand request, CancellationToken cancellationToken)
        {
            var result = await _teacherRepository.RemoveCourseTeacherAsync(request.TeacherId, request.CourseId);

            return new Response<bool>(result);
        }
    }
}