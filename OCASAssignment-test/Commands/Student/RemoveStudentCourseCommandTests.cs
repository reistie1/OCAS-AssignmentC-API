using Moq;
using OCAS.Domain.Common;
using OCASAPI.Application.DTO.Common;
using OCASAPI.Application.Features;
using OCASAPI.Application.Wrappers;

namespace OCASAPI_Tests.Commands
{
    public class RemoveStudentCourseCommandTests
    {
        private readonly Mock<IStudentRepository> _mockStudentRepo;
        private readonly StudentDto _mappedStudentDto;
        private readonly Student _mappedStudent;
        private readonly Guid _courseId;

        public RemoveStudentCourseCommandTests()
        {
            Guid SchoolId = Guid.NewGuid();
            Guid StudentId = Guid.NewGuid();
            _courseId = Guid.NewGuid();

            _mappedStudent = new Student(){Id = StudentId, FirstName = "Sample", LastName = "Student", SchoolId = SchoolId};
            _mappedStudentDto = new StudentDto(){Id = StudentId, FirstName = "Sample", LastName = "Student", SchoolId = SchoolId};
            
            _mockStudentRepo = new Mock<IStudentRepository>();
            _mockStudentRepo.Setup(s => s.RemoveCourseAsync(_mappedStudent.Id, _courseId)).ReturnsAsync(true);
        }

        [Fact]
        public async Task RemoveStudentCourseCommand_ReturnsTrue()
        {
            var command = new RemoveStudentCourseCommand(_mappedStudent.Id, _courseId);
            var handler = new RemoveStudentCourseCommandHandler(_mockStudentRepo.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.IsType<Response<bool>>(result);
            Assert.True(result.Data);
        }
    }
}