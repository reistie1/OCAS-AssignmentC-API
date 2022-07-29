using AutoMapper;
using Moq;
using OCAS.Domain.Common;
using OCASAPI.Application.Features;
using OCASAPI.Application.Interfaces;
using OCASAPI.Application.Requests;

namespace OCASAPI.Tests.Commands
{
    public class GetActivityListCommandTests
    {
        private readonly Mock<IActivityRepository> _mockActivityRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly List<Activity> _activityList;
        private readonly List<ActivityDto> _activityDtoList;
        public GetActivityListCommandTests()
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

            _mockActivityRepo = new Mock<IActivityRepository>();
            _mockActivityRepo.Setup(a => a.GetActivityListAsync()).ReturnsAsync(_activityList);


            _mockMapper = new Mock<IMapper>();
            _mockMapper.Setup(m => m.Map<IReadOnlyList<ActivityDto>>(_activityList)).Returns(_activityDtoList);
        }

        [Fact]
        public async Task GetActivityListCommand_ReturnsActivityList()
        {
            var command = new GetActivityListCommand();
            var handler = new GetActivityListCommandHandler(_mockMapper.Object, _mockActivityRepo.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.IsType<List<ActivityDto>>(result.Data);
            Assert.NotEmpty(result.Data);
            Assert.Equal(_activityDtoList, result.Data);
        }
    }
}