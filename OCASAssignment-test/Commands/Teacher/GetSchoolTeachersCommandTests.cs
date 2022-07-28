using Moq;
using AutoMapper;
using OCAS.Domain.Common;
using OCASAPI.Application.DTO.Common;
using OCASAPI.Application.Features;
using OCASAPI.Application.Wrappers;
using OCASAPI.Application.Validators;

namespace OCASAPI_Tests.Commands
{
    public class GetSchoolTeachersCommandTests
    {
        private readonly Mock<ITeacherRepository> _mockTeacherRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly List<TeacherDto> _mappedTeacherDto;
        private readonly List<Teacher> _mappedTeacher;

        public GetSchoolTeachersCommandTests()
        {
            Guid SchoolId = Guid.NewGuid();
            Guid TeacherId = Guid.NewGuid();


            _mappedTeacher = new List<Teacher>(){
                new Teacher(){Id = TeacherId, FirstName = "Sample", LastName = "Teacher", SubjectClassifier = "Science", SchoolId = SchoolId},
                new Teacher(){Id = Guid.NewGuid(), FirstName = "Sample", LastName = "Teacher", SubjectClassifier = "Science", SchoolId = SchoolId},
                new Teacher(){Id = Guid.NewGuid(), FirstName = "Sample", LastName = "Teacher", SubjectClassifier = "Science", SchoolId = SchoolId},
                new Teacher(){Id = Guid.NewGuid(), FirstName = "Sample", LastName = "Teacher", SubjectClassifier = "Science", SchoolId = SchoolId},
            };

            _mappedTeacherDto = new List<TeacherDto>(){
                new TeacherDto(){Id = TeacherId, FirstName = "Sample", LastName = "Teacher", SubjectClassifier = "Science", SchoolId = SchoolId},
                new TeacherDto(){Id = Guid.NewGuid(), FirstName = "Sample", LastName = "Teacher", SubjectClassifier = "Science", SchoolId = SchoolId},
                new TeacherDto(){Id = Guid.NewGuid(), FirstName = "Sample", LastName = "Teacher", SubjectClassifier = "Science", SchoolId = SchoolId},
                new TeacherDto(){Id = Guid.NewGuid(), FirstName = "Sample", LastName = "Teacher", SubjectClassifier = "Science", SchoolId = SchoolId},
            };

            _mockTeacherRepo = new Mock<ITeacherRepository>();
            _mockTeacherRepo.Setup(m => m.GetSchoolTeachersAsync(t => t.SchoolId == SchoolId)).ReturnsAsync(_mappedTeacher);

           _mockMapper = new Mock<IMapper>();
           _mockMapper.Setup(m => m.Map<IReadOnlyList<TeacherDto>>(It.IsAny<IReadOnlyList<Teacher>>())).Returns(_mappedTeacherDto);
        }

        [Fact]
        public async Task GetSchoolTeachersCommand_ReturnsTeacherList()
        {
            var command = new GetSchoolTeachersCommand(_mappedTeacher.First().SchoolId);
            var handler = new GetSchoolTeachersCommandHandler(_mockMapper.Object, _mockTeacherRepo.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.IsType<Response<IReadOnlyList<TeacherDto>>>(result);
            Assert.Same(_mappedTeacherDto, result.Data);
            Assert.NotEmpty(result.Data);
        }
    }
}