using Moq;
using AutoMapper;
using OCAS.Domain.Common;
using OCASAPI.Application.DTO.Common;
using OCASAPI.Application.Features;
using OCASAPI.Application.Wrappers;
using OCASAPI.Application.Validators;

namespace OCASAPI_Tests.Commands
{
    public class UpdateStudentCommandTests
    {
        private readonly Mock<IStudentRepository> _mockStudentRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly StudentDto _mappedStudentDto;
        private readonly Student _mappedStudent;

        public UpdateStudentCommandTests()
        {
            Guid SchoolId = Guid.NewGuid();
            Guid StudentId = Guid.NewGuid();

            _mappedStudent = new Student(){Id = StudentId, FirstName = "Sample", LastName = "Student", SchoolId = SchoolId};
            _mappedStudentDto = new StudentDto(){Id = StudentId, FirstName = "Sample", LastName = "Student", SchoolId = SchoolId};
            
            _mockStudentRepo = new Mock<IStudentRepository>();
            _mockStudentRepo.Setup(s => s.UpdateStudentAsync(_mappedStudent)).ReturnsAsync(_mappedStudent);

            _mockMapper = new Mock<IMapper>();
            _mockMapper.Setup(m => m.Map<StudentDto>(_mappedStudent)).Returns(_mappedStudentDto);
            _mockMapper.Setup(m => m.Map<Student>(_mappedStudentDto)).Returns(_mappedStudent);
        }

        [Fact]
        public async Task UpdateStudentCommand_ReturnsUpdatedStudentEntity()
        {
            var command = new UpdateSchoolStudentCommand(_mappedStudentDto);
            var handler = new UpdateSchoolStudentCommandHandler(_mockMapper.Object, _mockStudentRepo.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.IsType<Response<StudentDto>>(result);
            Assert.NotNull(result.Data);
            Assert.Same(_mappedStudentDto, result.Data);
        }

        [Fact]
        public async Task UpdateStudentCommand_ValidatesRequestProperties()
        {
            var command = new UpdateSchoolStudentCommand(new StudentDto(){FirstName = "john%%", LastName = "smith^&&", Age = 101, SchoolId = Guid.Empty});
            var validator = new UpdateSchoolStudentCommandValidator();
            var result = await validator.ValidateAsync(command);

            Assert.Equal(4, result.Errors.Count);
        }
    }
}