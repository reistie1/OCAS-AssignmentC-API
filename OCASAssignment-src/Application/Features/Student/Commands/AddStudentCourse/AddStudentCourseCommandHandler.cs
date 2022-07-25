using MediatR;
using OCASAPI.Application.DTO.Common;
using OCASAPI.Application.Wrappers;

namespace OCASAPI.Application.Features
{
    public class AddStudentCourseCommandHandler : IRequestHandler<AddStudentCourseCommand,Response<bool>>
    {
        private readonly IStudentRepository _studentRepository;

        public AddStudentCourseCommandHandler(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<Response<bool>> Handle(AddStudentCourseCommand request, CancellationToken cancellationToken)
        {
            var result = await _studentRepository.AddCourseAsync(request.StudentId, request.CourseId);

            return new Response<bool>(result);
        }
    }
}