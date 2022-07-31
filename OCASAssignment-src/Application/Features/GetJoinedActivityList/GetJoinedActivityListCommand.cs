using MediatR;
using OCASAPI.Application.Parameters;
using OCASAPI.Application.Requests;
using OCASAPI.Application.Wrappers;

namespace OCASAPI.Application.Features
{
    public class GetJoinedActivityListCommand : IRequest<PagedResponse<IReadOnlyList<ActivityPersonResponse>>>
    {
        public GetJoinedActivityListCommand(Guid activityId, RequestParameters requestParams)
        {
            ActivityId = activityId;
            RequestParams = requestParams;
        }

        public Guid ActivityId { get; }
        public RequestParameters RequestParams { get; }
    }
}