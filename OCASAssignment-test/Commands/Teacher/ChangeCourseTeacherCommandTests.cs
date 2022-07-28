using Moq;
using OCASAPI.Application.Features;
using OCASAPI.Application.Wrappers;
using OCASAPI.Application.DTO.Requests;

namespace OCASAPI_Tests.Commands
{
    public class ChangeCourseTeacherCommandTests
    {
        private readonly Mock<ITeacherRepository> _mockTeacherRepo;
        private readonly ChangeTeacherRequest _request;

        public ChangeCourseTeacherCommandTests()
        {
            _request = new ChangeTeacherRequest(){
                CourseId = Guid.NewGuid(),
                OldTeacherId = Guid.NewGuid(),
                NewTeacherId = Guid.NewGuid()
            };

            _mockTeacherRepo = new Mock<ITeacherRepository>();
            _mockTeacherRepo.Setup(m => m.ChangeCourseTeacherAsync(_request)).ReturnsAsync(true);
        }

        [Fact]
        public async Task ChangeCourseTeacherCommand_ReturnsTrue()
        {
            var command = new ChangeCourseTeacherCommand(_request);
            var handler = new ChangeCourseTeacherCommandHandler(_mockTeacherRepo.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.IsType<Response<bool>>(result);
            Assert.True(result.Data);
        }
    }
}