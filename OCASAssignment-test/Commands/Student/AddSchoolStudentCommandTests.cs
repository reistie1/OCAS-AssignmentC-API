using Moq;
using AutoMapper;
using OCAS.Domain.Common;
using OCASAPI.Application.DTO.Common;
using OCASAPI.Application.Features;
using OCASAPI.Application.Wrappers;
using OCASAPI.Application.Validators;

namespace OCASAPI_Tests.Commands
{
    public class AddStudentCommandTests
    {
        private readonly Mock<IStudentRepository> _mockStudentRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly StudentDto _mappedStudentDto;
        private readonly Student _mappedStudent;

        public AddStudentCommandTests()
        {
            Guid SchoolId = Guid.NewGuid();
            Guid StudentId = Guid.NewGuid();

            _mappedStudent = new Student(){Id = StudentId, FirstName = "Sample", LastName = "Student", SchoolId = SchoolId};
            _mappedStudentDto = new StudentDto(){Id = StudentId, FirstName = "Sample", LastName = "Student", SchoolId = SchoolId};
            
            _mockStudentRepo = new Mock<IStudentRepository>();
            _mockStudentRepo.Setup(s => s.AddStudentAsync(_mappedStudent)).ReturnsAsync(_mappedStudent);

            _mockMapper = new Mock<IMapper>();
            _mockMapper.Setup(m => m.Map<StudentDto>(_mappedStudent)).Returns(_mappedStudentDto);
            _mockMapper.Setup(m => m.Map<Student>(_mappedStudentDto)).Returns(_mappedStudent);
        }

        [Fact]
        public async Task AddStudentCommand_ReturnsAddedStudentEntity()
        {
            var command = new AddSchoolStudentCommand(_mappedStudentDto);
            var handler = new AddSchoolStudentCommandHandler(_mockMapper.Object, _mockStudentRepo.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.IsType<Response<StudentDto>>(result);
            Assert.NotNull(result.Data);
            Assert.Same(_mappedStudentDto, result.Data);
        }

        [Fact]
        public async Task AddStudentCommand_ValidatesRequestProperties()
        {
            var command = new AddSchoolStudentCommand(new StudentDto(){FirstName = "john%%", LastName = "smith^&&", Age = 101, SchoolId = Guid.Empty});
            var validator = new AddSchoolStudentCommandValidator();
            var result = await validator.ValidateAsync(command);

            Assert.Equal(4, result.Errors.Count);
        }
    }
}