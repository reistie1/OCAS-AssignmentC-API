using AutoMapper;
using MediatR;
using OCASAPI.Application.Interfaces;
using OCASAPI.Application.Requests;
using OCASAPI.Application.Wrappers;

namespace OCASAPI.Application.Features
{
    public class GetActivityListCommandHandler : IRequestHandler<GetActivityListCommand, Response<IReadOnlyList<ActivityDto>>>
    {
        private readonly IActivityRepository _activityRepository;
        private readonly IMapper _mapper;

        public GetActivityListCommandHandler(IMapper mapper, IActivityRepository activityRepository)
        {
            _activityRepository = activityRepository;
            _mapper = mapper;
        }
        public async Task<Response<IReadOnlyList<ActivityDto>>> Handle(GetActivityListCommand request, CancellationToken cancellationToken)
        {
            var result = await _activityRepository.GetActivityListAsync();

            return new Response<IReadOnlyList<ActivityDto>>(_mapper.Map<IReadOnlyList<ActivityDto>>(result));
        }
    }
}