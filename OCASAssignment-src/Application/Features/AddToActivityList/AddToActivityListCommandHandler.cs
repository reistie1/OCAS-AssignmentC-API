using AutoMapper;
using MediatR;
using OCAS.Domain.Common;
using OCASAPI.Application.Interfaces;
using OCASAPI.Application.Wrappers;

namespace OCASAPI.Application.Features
{
    public class AddToActivityCommandHandler : IRequestHandler<AddToActivityListCommand, Response<bool>>
    {
        private readonly IActivityRepository _activityRepository;
        private readonly IMapper _mapper;

        public AddToActivityCommandHandler(IMapper mapper, IActivityRepository activityRepository)
        {
            _activityRepository = activityRepository;
            _mapper = mapper;
        }
        public async Task<Response<bool>> Handle(AddToActivityListCommand request, CancellationToken cancellationToken)
        {
            var result = await _activityRepository.AddPersonToActivityAsync(_mapper.Map<ActivitySignUp>(request.Person));

            return new Response<bool>(result);
        }
    }
}