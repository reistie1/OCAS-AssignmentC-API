using MediatR;
using OCASAPI.Application.DTO.Common;
using OCASAPI.Application.Wrappers;

namespace OCASAPI.Application.Features
{
    
    public class DeleteCourseCommand : IRequest<Response<bool>>
    {
        public DeleteCourseCommand(Guid courseId)
        {
            CourseId = courseId;
        }

        public Guid CourseId { get; }
    }
}