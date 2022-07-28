using AutoMapper;
using MediatR;
using OCAS.Domain.Common;
using OCASAPI.Application.DTO.Common;
using OCASAPI.Application.Wrappers;

namespace OCASAPI.Application.Features
{
    
    public class EditCourseCommandHandler : IRequestHandler<EditCourseCommand, Response<CourseDto>>
    {
        private IMapper _mapper;
        private ICourseRepository _courseRepository;
        
        public EditCourseCommandHandler(IMapper mapper, ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
            _mapper = mapper;
        }


        public async Task<Response<CourseDto>> Handle(EditCourseCommand request, CancellationToken cancellationToken)
        {
            var result = await _courseRepository.EditCourseAsync(_mapper.Map<Course>(request.Course));

            return new Response<CourseDto>(_mapper.Map<CourseDto>(result));
        }
    }
}