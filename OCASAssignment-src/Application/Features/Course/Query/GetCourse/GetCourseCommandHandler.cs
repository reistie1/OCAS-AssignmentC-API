using AutoMapper;
using MediatR;
using OCASAPI.Application.DTO.Common;
using OCASAPI.Application.Wrappers;

namespace OCASAPI.Application.Features
{
    
    public class GetCourseCommandHandler : IRequestHandler<GetCourseCommand, Response<CourseDto>>
    {
        private IMapper _mapper;
        private ICourseRepository _courseRepository;
        
        public GetCourseCommandHandler(IMapper mapper, ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
            _mapper = mapper;
        }


        public async Task< Response<CourseDto>> Handle(GetCourseCommand request, CancellationToken cancellationToken)
        {
            var result = await _courseRepository.GetCourseAsync(request.CourseId);

            return new Response<CourseDto>(_mapper.Map<CourseDto>(result));
        }
    }
}