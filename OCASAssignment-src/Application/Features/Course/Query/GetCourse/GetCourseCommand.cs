using MediatR;
using OCASAPI.Application.DTO.Common;
using OCASAPI.Application.Wrappers;

namespace OCASAPI.Application.Features
{
    
    public class GetCourseCommand : IRequest<Response<CourseDto>>
    {
        public GetCourseCommand(Guid courseId)
        {
            CourseId = courseId;
        }

        public Guid CourseId{ get; }
    }
}