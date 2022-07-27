using MediatR;
using OCASAPI.Application.DTO.Common;
using OCASAPI.Application.Wrappers;

namespace OCASAPI.Application.Features
{
    public class DeleteSchoolTeacherCommandHandler : IRequestHandler<DeleteSchoolTeacherCommand,Response<bool>>
    {
        private readonly ITeacherRepository _teacherRepository;

        public DeleteSchoolTeacherCommandHandler(ITeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }

        public async Task<Response<bool>> Handle(DeleteSchoolTeacherCommand request, CancellationToken cancellationToken)
        {
            var result = await _teacherRepository.DeleteTeacherAsync(request.TeacherId);

            return new Response<bool>(result);
        }
    }
}