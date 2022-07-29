using MediatR;
using OCASAPI.Application.Requests;
using OCASAPI.Application.Wrappers;

namespace OCASAPI.Application.Features
{
    public class GetJoinedActivityListCommand : IRequest<Response<IReadOnlyList<ActivityPersonResponse>>>
    {
        public GetJoinedActivityListCommand(Guid activityId)
        {
            ActivityId = activityId;
        }

        public Guid ActivityId { get; }
    }
}