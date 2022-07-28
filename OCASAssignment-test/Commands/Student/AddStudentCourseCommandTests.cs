using Moq;
using OCAS.Domain.Common;
using OCASAPI.Application.DTO.Common;
using OCASAPI.Application.Features;
using OCASAPI.Application.Wrappers;
using OCASAPI.Application.Validators;

namespace OCASAPI_Tests.Commands
{
    public class AddStudentCourseCommandTests
    {
        private readonly Mock<IStudentRepository> _mockStudentRepo;
        private readonly StudentDto _mappedStudentDto;
        private readonly Student _mappedStudent;
        private readonly Guid _courseId;

        public AddStudentCourseCommandTests()
        {
            Guid SchoolId = Guid.NewGuid();
            Guid StudentId = Guid.NewGuid();
            _courseId = Guid.NewGuid();

            _mappedStudent = new Student(){Id = StudentId, FirstName = "Sample", LastName = "Student", SchoolId = SchoolId};
            _mappedStudentDto = new StudentDto(){Id = StudentId, FirstName = "Sample", LastName = "Student", SchoolId = SchoolId};
            
            _mockStudentRepo = new Mock<IStudentRepository>();
            _mockStudentRepo.Setup(s => s.AddCourseAsync(_mappedStudent.Id, _courseId)).ReturnsAsync(true);
        }

        [Fact]
        public async Task AddStudentCourseCommand_ReturnsStatusOfRequest()
        {
            var command = new AddStudentCourseCommand(_mappedStudentDto.Id, _courseId);
            var handler = new AddStudentCourseCommandHandler(_mockStudentRepo.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.IsType<Response<bool>>(result);
            Assert.True(result.Data);
        }

        [Fact]
        public async Task AddStudentCourseCommand_ValidatesRequestProperties()
        {
            var command = new AddStudentCourseCommand(Guid.Empty, Guid.Empty);
            var validator = new AddStudentCourseCommandValidator();
            var result = await validator.ValidateAsync(command);

            Assert.Equal(2, result.Errors.Count);
        }
    }
}