using MediatR;
using OCASAPI.Application.DTO.Common;
using OCASAPI.Application.Wrappers;

namespace OCASAPI.Application.Features
{
    
    public class AddCourseCommand : IRequest<Response<CourseDto>>
    {
        public AddCourseCommand(CourseDto course)
        {
            Course = course;
        }

        public CourseDto Course { get; }
    }
}