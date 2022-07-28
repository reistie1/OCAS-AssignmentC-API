using AutoMapper;
using MediatR;
using OCASAPI.Application.DTO.Common;
using OCASAPI.Application.Wrappers;

namespace OCASAPI.Application.Features
{
    public class DeleteStudentGradeCommandHandler : IRequestHandler<DeleteStudentGradeCommand,Response<bool>>
    {
        private readonly IMapper _mapper;
        private readonly IGradeRepository _gradeRepository;

        public DeleteStudentGradeCommandHandler(IMapper mapper, IGradeRepository gradeRepository)
        {
            _mapper = mapper;
            _gradeRepository = gradeRepository;
        }

        public async Task<Response<bool>> Handle(DeleteStudentGradeCommand request, CancellationToken cancellationToken)
        {
            var result = await _gradeRepository.DeleteStudentGradeAsync(request.StudentId, request.CourseId);

            return new Response<bool>(result);
        }
    }
}