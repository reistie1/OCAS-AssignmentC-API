using AutoMapper;
using Moq;
using OCAS.Domain.Common;
using OCASAPI.Application.Features;
using OCASAPI.Application.Interfaces;
using OCASAPI.Application.Requests;
using OCASAPI.Application.Validators;

namespace OCASAPI.Tests.Commands
{
    public class AddPersonToActivityCommandTests
    {
        private readonly Mock<IActivityRepository> _mockActivityRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly ActivitySignUp _personToAddEntity;
        private readonly ActivityPersonRequest _personAddRequest;
        private readonly ActivityPersonResponse _personAddResponse;
        public AddPersonToActivityCommandTests()
        {
            _personAddRequest = new ActivityPersonRequest(){FirstName = "John", LastName = "Smith", Email = "test@gmail.com"};
            _personAddResponse = new ActivityPersonResponse(){FirstName = "John", LastName = "Smith", Email = "test@gmail.com"};
            _personToAddEntity = new ActivitySignUp(){Id = Guid.NewGuid(), LastName = "Smith", Email = "test@gmail.com", ActivityId = Guid.NewGuid()};

            _mockActivityRepo = new Mock<IActivityRepository>();
            _mockActivityRepo.Setup(a => a.AddPersonToActivityAsync(_personToAddEntity)).ReturnsAsync(true);


            _mockMapper = new Mock<IMapper>();
            _mockMapper.Setup(m => m.Map<ActivityPersonResponse>(_personToAddEntity)).Returns(_personAddResponse);
            _mockMapper.Setup(m => m.Map<ActivitySignUp>(_personAddRequest)).Returns(_personToAddEntity);

        }

        [Fact]
        public async Task AddPersonToActivity_ReturnsTrue()
        {
            var command = new AddToActivityListCommand(_personAddRequest);
            var handler = new AddToActivityCommandHandler(_mockMapper.Object, _mockActivityRepo.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.IsType<bool>(result.Data);
            Assert.True(result.Data);
        }

        [Fact]
        public async Task AddPersonToActivityValidator_ValidatesRequest()
        {
            var command = new AddToActivityListCommand(new ActivityPersonRequest(){FirstName = "John$$%%#@!#", LastName = "Smith$%^&**#$@#", Comments = "something long here $%^$#@$!@%#^@$%&^@"});
            var validator = new AddToActivityListCommandValidator();
            var result = await validator.ValidateAsync(command);

            Assert.Equal(5, result.Errors.Count);
        }
    }
}