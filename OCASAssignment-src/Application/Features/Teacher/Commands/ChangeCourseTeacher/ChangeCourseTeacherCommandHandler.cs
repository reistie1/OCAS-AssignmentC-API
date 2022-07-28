using AutoMapper;
using MediatR;
using OCASAPI.Application.DTO.Common;
using OCASAPI.Application.Wrappers;

namespace OCASAPI.Application.Features
{
    public class ChangeCourseTeacherCommandHandler : IRequestHandler<ChangeCourseTeacherCommand,Response<bool>>
    {
        private readonly ITeacherRepository _teacherRepository;

        public ChangeCourseTeacherCommandHandler(ITeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }

        public async Task<Response<bool>> Handle(ChangeCourseTeacherCommand request, CancellationToken cancellationToken)
        {
            var result = await _teacherRepository.ChangeCourseTeacherAsync(request.TeacherChange);

            return new Response<bool>(result);
        }
    }
}