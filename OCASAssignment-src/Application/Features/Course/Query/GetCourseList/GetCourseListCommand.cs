using MediatR;
using OCASAPI.Application.DTO.Common;
using OCASAPI.Application.Parameters;
using OCASAPI.Application.Wrappers;

namespace OCASAPI.Application.Features
{
    
    public class GetCourseListCommand : IRequest<Response<IReadOnlyList<CourseDto>>>
    {
        public GetCourseListCommand(Guid schoolId, RequestParameters requestParameters)
        {
            SchoolId = schoolId;
            RequestParameters = requestParameters;
        }

        public Guid SchoolId{ get; private set;}
        public RequestParameters RequestParameters {get; private set;}
    }
}