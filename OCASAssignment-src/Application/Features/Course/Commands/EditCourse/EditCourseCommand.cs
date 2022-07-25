using MediatR;
using OCASAPI.Application.DTO.Common;
using OCASAPI.Application.Wrappers;

namespace OCASAPI.Application.Features
{
    
    public class EditCourseCommand : IRequest<Response<CourseDto>>
    {
        public EditCourseCommand(CourseDto course)
        {
            Course = course;
        }

        public CourseDto Course { get; }
    }
}