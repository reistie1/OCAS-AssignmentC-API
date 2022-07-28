using Moq;
using AutoMapper;
using OCASAPI.Data;
using OCAS.Domain.Common;
using OCASAPI.Application.DTO.Common;
using OCASAPI.Application.Features;
using OCASAPI.Application.Wrappers;
using OCASAPI.Application.Validators;

namespace OCASAPI_Tests.Commands
{
    public class AddCourseCommandTests
    {
        private readonly Mock<ICourseRepository> _mockCourseRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly SampleData _sampleData;
        private readonly CourseDto _mappedCourseDto;
        private readonly Course _mappedCourse;

        public AddCourseCommandTests()
        {
            Guid courseId = Guid.NewGuid();
            Guid schoolId = Guid.NewGuid();
            _mappedCourseDto = new CourseDto(){Id = courseId, CourseCode = "math-100", CourseName = "Math 100", Description = "Entry level math course covering trigonometry", SchoolId = schoolId};
            _mappedCourse = new Course(){Id = courseId, CourseCode = "math-100", CourseName = "Math 100", Description = "Entry level math course covering trigonometry", SchoolId = schoolId};

            _sampleData = new SampleData();
            _mockCourseRepo = new Mock<ICourseRepository>();
            _mockCourseRepo.Setup(c => c.AddCourseAsync(_mappedCourse)).ReturnsAsync(_mappedCourse);
            
            
            _mockMapper = new Mock<IMapper>();
            _mockMapper.Setup(m => m.Map<Course>(_mappedCourseDto)).Returns(_mappedCourse);
            _mockMapper.Setup(m => m.Map<CourseDto>(_mappedCourse)).Returns(_mappedCourseDto);
            
        }

        [Fact]
        public async Task AddCourseCommand_ReturnsAddedCourseEntity()
        {
            var command = new AddCourseCommand(_mappedCourseDto);
            var handler = new AddCourseCommandHandler(_mockMapper.Object, _mockCourseRepo.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.IsType<Response<CourseDto>>(result);
            Assert.NotNull(result);
            Assert.Same(_mappedCourseDto, result.Data);
        }

        [Fact]
        public async Task AddCourseCommand_ValidatesProperties()
        {
            CourseDto courseDto = new CourseDto();
            var command = new AddCourseCommand(courseDto);
            var validator = new AddCourseCommandValidator();
            var result = await validator.ValidateAsync(command);

            Assert.Equal(4, result.Errors.Count);
        }
    }
}