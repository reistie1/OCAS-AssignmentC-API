using AutoMapper;
using MediatR;
using OCAS.Domain.Common;
using OCASAPI.Application.DTO.Common;
using OCASAPI.Application.Wrappers;

namespace OCASAPI.Application.Features
{
    
    public class AddCourseCommandHandler : IRequestHandler<AddCourseCommand, Response<CourseDto>>
    {
        private IMapper _mapper;
        private ICourseRepository _courseRepository;
        
        public AddCourseCommandHandler(IMapper mapper, ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
            _mapper = mapper;
        }


        public async Task<Response<CourseDto>> Handle(AddCourseCommand request, CancellationToken cancellationToken)
        {
            var result = await _courseRepository.AddCourseAsync(_mapper.Map<Course>(request.Course));
            return new Response<CourseDto>(_mapper.Map<CourseDto>(request.Course));
        }
    }
}