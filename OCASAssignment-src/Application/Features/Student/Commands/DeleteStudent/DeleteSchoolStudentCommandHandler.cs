using MediatR;
using OCASAPI.Application.Wrappers;

namespace OCASAPI.Application.Features
{
    public class DeleteSchoolStudentCourseCommandHandler : IRequestHandler<DeleteSchoolStudentCourseCommand,Response<bool>>
    {
        private readonly IStudentRepository _studentRepository;

        public DeleteSchoolStudentCourseCommandHandler(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<Response<bool>> Handle(DeleteSchoolStudentCourseCommand request, CancellationToken cancellationToken)
        {
            var result = await _studentRepository.DeleteStudentAsync(request.StudentId);

            return new Response<bool>(result);
        }
    }
}