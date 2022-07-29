using AutoMapper;
using Moq;
using OCAS.Domain.Common;
using OCASAPI.Application.Features;
using OCASAPI.Application.Interfaces;
using OCASAPI.Application.Requests;
using OCASAPI.Application.Validators;

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
          

            _mockActivityRepo = new Mock<IActivityRepository>();
            _mockActivityRepo.Setup(a => a.GetActivityListAsync()).ReturnsAsync(true);


            _mockMapper = new Mock<IMapper>();
            

        }

        [Fact]
        public async Task AddPersonToActivity_ReturnsTrue()
        {
            // var command = new AddToActivityListCommand(_personAddRequest);
            // var handler = new AddToActivityCommandHandler(_mockMapper.Object, _mockActivityRepo.Object);
            // var result = await handler.Handle(command, CancellationToken.None);

            // Assert.IsType<bool>(result.Data);
            // Assert.True(result.Data);
        }

        [Fact]
        public async Task AddPersonToActivityValidator_ValidatesRequest()
        {
            // var command = new AddToActivityListCommand(new ActivityPersonRequest(){FirstName = "John$$%%#@!#", LastName = "Smith$%^&**#$@#", Comments = "something long here $%^$#@$!@%#^@$%&^@"});
            // var validator = new AddToActivityListCommandValidator();
            // var result = await validator.ValidateAsync(command);

            // Assert.Equal(5, result.Errors.Count);
        }
    }
}