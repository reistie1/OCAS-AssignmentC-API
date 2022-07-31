using AutoMapper;
using MediatR;
using OCASAPI.Application.Interfaces;
using OCASAPI.Application.Requests;
using OCASAPI.Application.Wrappers;

namespace OCASAPI.Application.Features
{
    public class GetJoinedActivityListCommandHandler : IRequestHandler<GetJoinedActivityListCommand, PagedResponse<IReadOnlyList<ActivityPersonResponse>>>
    {
        private readonly IActivityRepository _activityRepository;
        private readonly IMapper _mapper;

        public GetJoinedActivityListCommandHandler(IMapper mapper, IActivityRepository activityRepository)
        {
            _activityRepository = activityRepository;
            _mapper = mapper;
        }
        public async Task<PagedResponse<IReadOnlyList<ActivityPersonResponse>>> Handle(GetJoinedActivityListCommand request, CancellationToken cancellationToken)
        {
            var result = await _activityRepository.GetPeopleEnrolledInActivity(request.ActivityId, request.RequestParams);

            return new PagedResponse<IReadOnlyList<ActivityPersonResponse>>(_mapper.Map<IReadOnlyList<ActivityPersonResponse>>(result), request.RequestParams.PageNumber, request.RequestParams.PageSize,0);
        }
    }
}