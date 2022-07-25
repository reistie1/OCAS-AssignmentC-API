using AutoMapper;
using MediatR;
using OCAS.Domain.Common;
using OCASAPI.Application.DTO.Common;
using OCASAPI.Application.Wrappers;

namespace OCASAPI.Application.Features
{
    
    public class GetCourseListCommandHandler : IRequestHandler<GetCourseListCommand, Response<IReadOnlyList<CourseDto>>>
    {
        private IMapper _mapper;
        private ICourseRepository _courseRepository;
        
        public GetCourseListCommandHandler(IMapper mapper, ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
            _mapper = mapper;
        }


        public async Task< Response<IReadOnlyList<CourseDto>>> Handle(GetCourseListCommand request, CancellationToken cancellationToken)
        {
            var result = await _courseRepository.GetCourseListAsync(c => c.SchoolId == request.SchoolId, request.RequestParameters);

            return new Response<IReadOnlyList<CourseDto>>(_mapper.Map<IReadOnlyList<CourseDto>>(request));
        }
    }
}