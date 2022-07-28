using Moq;
using AutoMapper;
using OCAS.Domain.Common;
using OCASAPI.Application.DTO.Common;
using OCASAPI.Application.Features;
using OCASAPI.Application.Wrappers;

namespace OCASAPI_Tests.Commands
{
    public class GetStudentCommandTests
    {
        private readonly Mock<IStudentRepository> _mockStudentRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly StudentDto _mappedStudentDto;
        private readonly Student _mappedStudent;

        public GetStudentCommandTests()
        {
            Guid SchoolId = Guid.NewGuid();
            Guid StudentId = Guid.NewGuid();

            _mappedStudent = new Student(){Id = StudentId, FirstName = "Sample", LastName = "Student", SchoolId = SchoolId, Courses = new List<Course>(){}};


            _mappedStudentDto = new StudentDto(){Id = StudentId, FirstName = "Sample", LastName = "Student", SchoolId = SchoolId, Courses = new List<CourseDto>(){}};

            
            _mockStudentRepo = new Mock<IStudentRepository>();
            _mockStudentRepo.Setup(s => s.GetStudentAsync(_mappedStudent.Id)).ReturnsAsync(_mappedStudent);

            _mockMapper = new Mock<IMapper>();
            _mockMapper.Setup(m => m.Map<StudentDto>(_mappedStudent)).Returns(_mappedStudentDto);
        }

        [Fact]
        public async Task GetStudentCommand_ReturnsStudentEntity()
        {
            var command = new GetStudentCommand(_mappedStudentDto.Id);
            var handler = new GetStudentCommandHandler(_mockMapper.Object, _mockStudentRepo.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.IsType<Response<StudentDto>>(result);
            Assert.NotNull(result.Data);
            Assert.Same(_mappedStudentDto, result.Data);
        }
    }
}