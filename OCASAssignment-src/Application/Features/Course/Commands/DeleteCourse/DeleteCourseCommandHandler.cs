using AutoMapper;
using MediatR;
using OCAS.Domain.Common;
using OCASAPI.Application.DTO.Common;
using OCASAPI.Application.Wrappers;

namespace OCASAPI.Application.Features
{
    
    public class DeleteCourseCommandHandler : IRequestHandler<DeleteCourseCommand, Response<bool>>
    {
        private ICourseRepository _courseRepository;
        
        public DeleteCourseCommandHandler(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }


        public async Task<Response<bool>> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
        {
            var result = await _courseRepository.DeleteCourseAsync(request.CourseId);

            return new Response<bool>(result);
        }
    }
}