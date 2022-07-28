using MediatR;
using OCASAPI.Application.Wrappers;

namespace OCASAPI.Application.Features
{
    public class DeleteSchoolStudentCommandHandler : IRequestHandler<DeleteSchoolStudentCommand,Response<bool>>
    {
        private readonly IStudentRepository _studentRepository;

        public DeleteSchoolStudentCommandHandler(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<Response<bool>> Handle(DeleteSchoolStudentCommand request, CancellationToken cancellationToken)
        {
            var result = await _studentRepository.DeleteStudentAsync(request.StudentId);

            return new Response<bool>(result);
        }
    }
}