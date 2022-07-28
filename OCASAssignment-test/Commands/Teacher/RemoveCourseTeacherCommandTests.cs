using Moq;
using OCAS.Domain.Common;
using OCASAPI.Application.DTO.Common;
using OCASAPI.Application.Features;
using OCASAPI.Application.Wrappers;

namespace OCASAPI_Tests.Commands
{
    public class RemoveCourseTeacherCommandTests
    {
        private readonly Mock<ITeacherRepository> _mockTeacherRepo;
        private readonly Teacher _mappedTeacher;
        private readonly Guid _courseId;

        public RemoveCourseTeacherCommandTests()
        {
            Guid SchoolId = Guid.NewGuid();
            Guid TeacherId = Guid.NewGuid();
            _courseId = Guid.NewGuid();

            _mappedTeacher = new Teacher(){Id = TeacherId, FirstName = "Sample", LastName = "Teacher", SubjectClassifier = "Science", SchoolId = SchoolId};

            _mockTeacherRepo = new Mock<ITeacherRepository>();
            _mockTeacherRepo.Setup(m => m.RemoveCourseTeacherAsync(_mappedTeacher.Id, _courseId)).ReturnsAsync(true);
        }

        [Fact]
        public async Task RemoveCourseTeacherCommand_ReturnsTrue()
        {
            var command = new RemoveCourseTeacherCommand(_courseId, _mappedTeacher.Id);
            var handler = new RemoveCourseTeacherCommandHandler(_mockTeacherRepo.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.IsType<Response<bool>>(result);
            Assert.True(result.Data);
        }
    }
}