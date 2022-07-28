using Moq;
using AutoMapper;
using OCAS.Domain.Common;
using OCASAPI.Application.DTO.Common;
using OCASAPI.Application.Features;
using OCASAPI.Application.Wrappers;
using OCASAPI.Application.Validators;

namespace OCASAPI_Tests.Commands
{
    public class AddCourseTeacherCommandTests
    {
        private readonly Mock<ITeacherRepository> _mockTeacherRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly TeacherDto _mappedTeacherDto;
        private readonly Teacher _mappedTeacher;
        private readonly Guid _courseId;

        public AddCourseTeacherCommandTests()
        {
            Guid SchoolId = Guid.NewGuid();
            Guid TeacherId = Guid.NewGuid();
            _courseId = Guid.NewGuid();


            _mappedTeacher = new Teacher(){Id = TeacherId, FirstName = "Sample", LastName = "Teacher", SubjectClassifier = "Science", SchoolId = SchoolId};
            _mappedTeacherDto = new TeacherDto(){Id = TeacherId, FirstName = "Sample", LastName = "Teacher", SubjectClassifier = "Science", SchoolId = SchoolId};

            _mockTeacherRepo = new Mock<ITeacherRepository>();
            _mockTeacherRepo.Setup(m => m.AddCourseTeacherAsync(_courseId, _mappedTeacher.Id)).ReturnsAsync(true);

           _mockMapper = new Mock<IMapper>();
           _mockMapper.Setup(m => m.Map<TeacherDto>(_mappedTeacher)).Returns(_mappedTeacherDto);
           _mockMapper.Setup(m => m.Map<Teacher>(_mappedTeacherDto)).Returns(_mappedTeacher);
        }

        [Fact]
        public async Task AddCourseTeacherCommand_ReturnsTrue()
        {
            var command = new AddCourseTeacherCommand(_courseId, _mappedTeacher.Id);
            var handler = new AddCourseTeacherCommandHandler(_mockMapper.Object, _mockTeacherRepo.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.IsType<Response<bool>>(result);
            Assert.True(result.Data);
        }
    }
}