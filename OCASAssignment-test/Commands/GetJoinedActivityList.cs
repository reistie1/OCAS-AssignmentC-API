using AutoMapper;
using Moq;
using OCAS.Domain.Common;
using OCASAPI.Application.Features;
using OCASAPI.Application.Interfaces;
using OCASAPI.Application.Parameters;
using OCASAPI.Application.Requests;
using OCASAPI.Application.Validators;
using OCASAPI.Application.Wrappers;

namespace OCASAPI.Tests.Commands
{
    public class GetJoinedActivityListCommandTests
    {
        private readonly Mock<IActivityRepository> _mockActivityRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly List<Activity> _activityList;
        private readonly List<ActivityDto> _activityDtoList;
        private readonly List<ActivitySignUp> _singedUpActivityList;
        private readonly List<ActivityPersonResponse> _personListResponse;
        public GetJoinedActivityListCommandTests()
        {
            _activityList = new List<Activity>()
            {
                new Activity(){Id = Guid.NewGuid(), ActivityName = "Floor Hockey", Description = "A nice friendly game of floor hockey between employees"},
                new Activity(){Id = Guid.NewGuid(), ActivityName = "Three Pitch", Description = "A nice friendly game of baseball between departments"},
                new Activity(){Id = Guid.NewGuid(), ActivityName = "Basketball", Description = "A nice friendly game of basketball between employees"}
            };

            _activityDtoList = new List<ActivityDto>()
            {
                new ActivityDto(){Id = Guid.NewGuid(), ActivityName = "Floor Hockey", Description = "A nice friendly game of floor hockey between employees"},
                new ActivityDto(){Id = Guid.NewGuid(), ActivityName = "Three Pitch", Description = "A nice friendly game of baseball between departments"},
                new ActivityDto(){Id = Guid.NewGuid(), ActivityName = "Basketball", Description = "A nice friendly game of basketball between employees"}
            };

            _singedUpActivityList = new List<ActivitySignUp>()
            {
                new ActivitySignUp(){Id = Guid.NewGuid(), FirstName = "John", LastName = "Snow", ActivityId = _activityList.First().Id, Email = "test1@gmail.com"},
                new ActivitySignUp(){Id = Guid.NewGuid(), FirstName = "Scott", LastName = "Pilgram", ActivityId = _activityList.First().Id, Email = "test2@gmail.com"},
                new ActivitySignUp(){Id = Guid.NewGuid(), FirstName = "Adam", LastName = "Scott", ActivityId = _activityList.Last().Id, Email = "test3@gmail.com"},
            };

            _personListResponse = new List<ActivityPersonResponse>()
            {
                new ActivityPersonResponse(){Id = Guid.NewGuid(), FirstName = "John", LastName = "Snow", ActivityId = _activityList.First().Id, Email = "test1@gmail.com"},
                new ActivityPersonResponse(){Id = Guid.NewGuid(), FirstName = "Scott", LastName = "Pilgram", ActivityId = _activityList.First().Id, Email = "test2@gmail.com"},
                new ActivityPersonResponse(){Id = Guid.NewGuid(), FirstName = "Adam", LastName = "Scott", ActivityId = _activityList.Last().Id, Email = "test3@gmail.com"},
            };;

            _mockActivityRepo = new Mock<IActivityRepository>();
            _mockActivityRepo.Setup(a => a.GetPeopleEnrolledInActivity(_activityList.First().Id, It.IsAny<RequestParameters>())).ReturnsAsync(_singedUpActivityList);


            _mockMapper = new Mock<IMapper>();
            _mockMapper.Setup(m => m.Map<IReadOnlyList<ActivityPersonResponse>>(_singedUpActivityList)).Returns(_personListResponse);
        }

        [Fact]
        public async Task GetJoinedActivityList_ReturnsListOfPersonEnrolledInActivityList()
        {
            var command = new GetJoinedActivityListCommand(_activityList.First().Id, new RequestParameters(1, 50, null));
            var handler = new GetJoinedActivityListCommandHandler(_mockMapper.Object, _mockActivityRepo.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.IsType<List<ActivityPersonResponse>>(result.Data);
            Assert.IsType<PagedResponse<IReadOnlyList<ActivityPersonResponse>>>(result);
            Assert.NotEmpty(result.Data);
            Assert.Equal(_personListResponse, result.Data);
        }

        [Fact]
        public async Task GetJoinedActivityList_ValidatesRequest()
        {
            var command = new GetJoinedActivityListCommand(Guid.Empty, new RequestParameters(1, 50, null));
            var validator = new GetJoinedActivityListCommandValidator();
            var result = await validator.ValidateAsync(command);

            Assert.Equal(1, result.Errors.Count);
        }
        
    }
}