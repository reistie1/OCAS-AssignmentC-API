using Moq;
using OCAS.Domain.Common;
using OCASAPI.Application.DTO.Common;
using OCASAPI.Application.Features;
using OCASAPI.Application.Wrappers;

namespace OCASAPI_Tests.Commands
{
    public class DeleteTeacherCommandTests
    {
        private readonly Mock<ITeacherRepository> _mockTeacherRepo;
        private readonly Teacher _mappedTeacher;

        public DeleteTeacherCommandTests()
        {
            Guid SchoolId = Guid.NewGuid();
            Guid TeacherId = Guid.NewGuid();

            _mappedTeacher = new Teacher(){Id = TeacherId, FirstName = "Sample", LastName = "Teacher", SubjectClassifier = "Science", SchoolId = SchoolId};

            _mockTeacherRepo = new Mock<ITeacherRepository>();
            _mockTeacherRepo.Setup(m => m.DeleteTeacherAsync(_mappedTeacher.Id)).ReturnsAsync(true);
        }

        [Fact]
        public async Task DeleteTeacherCommand_ReturnsTrue()
        {
            var command = new DeleteSchoolTeacherCommand(_mappedTeacher.Id);
            var handler = new DeleteSchoolTeacherCommandHandler(_mockTeacherRepo.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.IsType<Response<bool>>(result);
            Assert.True(result.Data);
        }
    }
}