using MediatR;
using OCASAPI.Application.Wrappers;

namespace OCASAPI.Application.Features
{
    public class RemoveStudentCourseCommandHandler : IRequestHandler<RemoveStudentCourseCommand,Response<bool>>
    {
        private readonly IStudentRepository _studentRepository;

        public RemoveStudentCourseCommandHandler(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<Response<bool>> Handle(RemoveStudentCourseCommand request, CancellationToken cancellationToken)
        {
            var result = await _studentRepository.RemoveCourseAsync(request.StudentId, request.CourseId);

            return new Response<bool>(result);
        }
    }
}