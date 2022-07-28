using Moq;
using AutoMapper;
using OCAS.Domain.Common;
using OCASAPI.Application.DTO.Common;
using OCASAPI.Application.Features;
using OCASAPI.Application.Wrappers;

namespace OCASAPI_Tests.Commands
{
    public class GetCourseCommandTests
    {
        private readonly Mock<ICourseRepository> _mockCourseRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly CourseDto _mappedCourseDto;
        private readonly Course _mappedCourse;

        public GetCourseCommandTests()
        {
            Guid courseId = Guid.NewGuid();
            Guid schoolId = Guid.NewGuid();

            _mappedCourseDto = new CourseDto(){Id = courseId, CourseCode = "math-100", CourseName = "Math 100", Description = "Entry level math course covering trigonometry", SchoolId = schoolId};
            
            _mappedCourse = new Course(){Id = courseId, CourseCode = "math-100", CourseName = "Math 100", Description = "Entry level math course covering trigonometry", SchoolId = schoolId};

            _mockCourseRepo = new Mock<ICourseRepository>();
            _mockCourseRepo.Setup(c => c.GetCourseAsync(_mappedCourse.Id)).ReturnsAsync(_mappedCourse);

            _mockMapper = new Mock<IMapper>();
            _mockMapper.Setup(m => m.Map<CourseDto>(_mappedCourse)).Returns(_mappedCourseDto);           
        }

        [Fact]
        public async Task GetCourseCommand_ReturnsResult()
        {
            var command = new GetCourseCommand(_mappedCourse.Id);
            var handler = new GetCourseCommandHandler(_mockMapper.Object, _mockCourseRepo.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.NotNull(result.Data);
            Assert.IsType<Response<CourseDto>>(result);
            Assert.Same(_mappedCourseDto, result.Data);
        }
    }
}