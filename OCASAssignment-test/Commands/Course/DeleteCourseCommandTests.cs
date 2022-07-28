using Moq;
using OCAS.Domain.Common;
using OCASAPI.Application.DTO.Common;
using OCASAPI.Application.Features;
using OCASAPI.Application.Wrappers;

namespace OCASAPI_Tests.Commands
{
    public class DeleteCourseCommandTests
    {
        private readonly Mock<ICourseRepository> _mockCourseRepo;
        private readonly CourseDto _mappedCourseDto;
        private readonly Course _mappedCourse;

        public DeleteCourseCommandTests()
        {
            Guid courseId = Guid.NewGuid();
            Guid schoolId = Guid.NewGuid();

            _mappedCourseDto = new CourseDto(){Id = courseId, CourseCode = "math-100", CourseName = "Math 100", Description = "Entry level math course covering trigonometry", SchoolId = schoolId};
            _mappedCourse = new Course(){Id = courseId, CourseCode = "math-100", CourseName = "Math 100", Description = "Entry level math course covering trigonometry", SchoolId = schoolId};

            _mockCourseRepo = new Mock<ICourseRepository>();
            _mockCourseRepo.Setup(c => c.DeleteCourseAsync(_mappedCourse.Id)).ReturnsAsync(true);            
        }

        [Fact]
        public async Task DeleteCourseCommand_ReturnsResult()
        {
            var command = new DeleteCourseCommand(_mappedCourse.Id);
            var handler = new DeleteCourseCommandHandler( _mockCourseRepo.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.True(result.Data);
            Assert.IsType<Response<bool>>(result);
        }
    }
}