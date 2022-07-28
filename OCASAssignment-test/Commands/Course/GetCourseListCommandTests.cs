using Moq;
using AutoMapper;
using OCAS.Domain.Common;
using OCASAPI.Application.DTO.Common;
using OCASAPI.Application.Features;
using OCASAPI.Application.Parameters;
using OCASAPI.Application.Wrappers;

namespace OCASAPI_Tests.Commands
{
    public class GetCourseListCommandTests
    {
        private readonly Mock<ICourseRepository> _mockCourseRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly List<CourseDto> _mappedCourseDto;
        private readonly Course _mappedCourse;

        public GetCourseListCommandTests()
        {
            Guid courseId = Guid.NewGuid();
            Guid schoolId = Guid.NewGuid();

            _mappedCourseDto = new List<CourseDto>(){
                new CourseDto(){Id = courseId, CourseCode = "math-100", CourseName = "Math 100", Description = "Entry level math course covering trigonometry", SchoolId = schoolId}
            };
            _mappedCourse = new Course(){Id = courseId, CourseCode = "math-100", CourseName = "Math 100", Description = "Entry level math course covering trigonometry", SchoolId = schoolId};

            _mockCourseRepo = new Mock<ICourseRepository>();
            _mockCourseRepo.Setup(c => c.GetCourseListAsync(t => t.SchoolId == schoolId, new RequestParameters(){PageNumber = 1, PageSize = 50}));

            _mockMapper = new Mock<IMapper>();
            _mockMapper.Setup(m => m.Map<IReadOnlyList<CourseDto>>(It.IsAny<List<Course>>())).Returns(_mappedCourseDto);           
        }

        [Fact]
        public async Task GetCourseListCommand_ReturnsResult()
        {
            var command = new GetCourseListCommand(_mappedCourse.SchoolId, new RequestParameters(){PageNumber = 1, PageSize = 50});
            var handler = new GetCourseListCommandHandler(_mockMapper.Object, _mockCourseRepo.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.NotEmpty(result.Data);
            Assert.Contains(_mappedCourseDto.First(), result.Data);
            Assert.IsType<Response<IReadOnlyList<CourseDto>>>(result);
        }
    }
}