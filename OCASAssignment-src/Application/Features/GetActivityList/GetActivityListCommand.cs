using MediatR;
using OCASAPI.Application.Requests;
using OCASAPI.Application.Wrappers;

namespace OCASAPI.Application.Features
{
    public class GetActivityListCommand : IRequest<Response<IReadOnlyList<ActivityDto>>>
    {
        public GetActivityListCommand(){}

    }
}