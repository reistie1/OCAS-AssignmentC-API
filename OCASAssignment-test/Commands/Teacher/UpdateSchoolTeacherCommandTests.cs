using Moq;
using AutoMapper;
using OCAS.Domain.Common;
using OCASAPI.Application.DTO.Common;
using OCASAPI.Application.Features;
using OCASAPI.Application.Wrappers;
using OCASAPI.Application.Validators;

namespace OCASAPI_Tests.Commands
{
    public class UpdateSchoolTeacherCommandTests
    {
        private readonly Mock<ITeacherRepository> _mockTeacherRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly TeacherDto _mappedTeacherDto;
        private readonly Teacher _mappedTeacher;

        public UpdateSchoolTeacherCommandTests()
        {
            Guid SchoolId = Guid.NewGuid();
            Guid TeacherId = Guid.NewGuid();

            _mappedTeacher = new Teacher(){Id = TeacherId, FirstName = "Sample", LastName = "Teacher", SubjectClassifier = "Science", SchoolId = SchoolId};
            _mappedTeacherDto = new TeacherDto(){Id = TeacherId, FirstName = "Sample", LastName = "Teacher", SubjectClassifier = "Science", SchoolId = SchoolId};

            _mockTeacherRepo = new Mock<ITeacherRepository>();
            _mockTeacherRepo.Setup(m => m.UpdateTeacherAsync(_mappedTeacher)).ReturnsAsync(_mappedTeacher);

           _mockMapper = new Mock<IMapper>();
           _mockMapper.Setup(m => m.Map<TeacherDto>(_mappedTeacher)).Returns(_mappedTeacherDto);
           _mockMapper.Setup(m => m.Map<Teacher>(_mappedTeacherDto)).Returns(_mappedTeacher);
        }

        [Fact]
        public async Task UpdateSchoolTeacherCommand_ReturnsUpdatedTeacherEntity()
        {
            var command = new UpdateSchoolTeacherCommand(_mappedTeacherDto);
            var handler = new UpdateSchoolTeacherCommandHandler(_mockMapper.Object, _mockTeacherRepo.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.IsType<Response<TeacherDto>>(result);
            Assert.NotNull(result.Data);
            Assert.Same(_mappedTeacherDto, result.Data);
        }

        [Fact]
        public async Task UpdateSchoolTeacherCommand_ValidatesRequestProperties()
        {
            var command = new UpdateSchoolTeacherCommand(new TeacherDto(){FirstName = "john%%", LastName = "smith^&&", SubjectClassifier = "Science*&%%%", SchoolId = Guid.Empty});
            var validator = new UpdateSchoolTeacherCommandValidator();
            var result = await validator.ValidateAsync(command);

            Assert.Equal(4, result.Errors.Count);   
        }
    }
}