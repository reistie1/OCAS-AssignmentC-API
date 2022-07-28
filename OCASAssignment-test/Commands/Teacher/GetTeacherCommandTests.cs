using Moq;
using AutoMapper;
using OCAS.Domain.Common;
using OCASAPI.Application.DTO.Common;
using OCASAPI.Application.Features;
using OCASAPI.Application.Wrappers;
using OCASAPI.Application.Validators;

namespace OCASAPI_Tests.Commands
{
    public class GetTeacherCommandTests
    {
        private readonly Mock<ITeacherRepository> _mockTeacherRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly TeacherDto _mappedTeacherDto;
        private readonly Teacher _mappedTeacher;
        private readonly Guid _courseId;

        public GetTeacherCommandTests()
        {
            Guid SchoolId = Guid.NewGuid();
            Guid TeacherId = Guid.NewGuid();
            _courseId = Guid.NewGuid();


            _mappedTeacher = new Teacher(){Id = TeacherId, FirstName = "Sample", LastName = "Teacher", SubjectClassifier = "Science", SchoolId = SchoolId};
            _mappedTeacherDto = new TeacherDto(){Id = TeacherId, FirstName = "Sample", LastName = "Teacher", SubjectClassifier = "Science", SchoolId = SchoolId};

            _mockTeacherRepo = new Mock<ITeacherRepository>();
            _mockTeacherRepo.Setup(m => m.GetTeacherAsync(_mappedTeacher.Id)).ReturnsAsync(_mappedTeacher);

           _mockMapper = new Mock<IMapper>();
           _mockMapper.Setup(m => m.Map<TeacherDto>(_mappedTeacher)).Returns(_mappedTeacherDto);
           _mockMapper.Setup(m => m.Map<Teacher>(_mappedTeacherDto)).Returns(_mappedTeacher);
        }

        [Fact]
        public async Task GetTeacherCommand_ReturnsTrue()
        {
            var command = new GetTeacherCommand(_mappedTeacher.Id);
            var handler = new GetTeacherCommandHandler(_mockMapper.Object, _mockTeacherRepo.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.IsType<Response<TeacherDto>>(result);
            Assert.Same(_mappedTeacherDto, result.Data);
            Assert.NotNull(result.Data);
        }
    }
}