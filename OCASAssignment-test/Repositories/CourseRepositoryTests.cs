using Microsoft.EntityFrameworkCore;
using OCAS.Domain.Common;
using OCASAPI.Application.Exceptions;
using OCASAPI.Application.Parameters;
using OCASAPI.Infrastructure.Context;
using OCASAPI.Infrastructure.Repositories;

namespace OCASAPI_Tests.Repository
{
    public class CourseRepositoryTest
    {
        private ApplicationContext _dbContext;
        private CourseRepository _courseRepo;
        private readonly List<Course> _courseList;
        private readonly Guid _schoolId;
        

        public CourseRepositoryTest()
        {
            _schoolId = Guid.NewGuid();

            _courseList = new List<Course>()
            {
                new Course(){Id = Guid.NewGuid(), CourseCode = "Math-100", CourseName = "Basic Mathematics", Description = "A starters math course to further ones basic math skills", SchoolId = _schoolId},
                new Course(){Id = Guid.NewGuid(), CourseCode = "Science-200", CourseName = "Advanced Science", Description = "A challenging science course to further ones advanced science skills", SchoolId = _schoolId},
                new Course(){Id = Guid.NewGuid(), CourseCode = "Gym-321", CourseName = "Games Gym", Description = "A fun gym course to play games and further cardiovascular health", SchoolId = _schoolId},
                new Course(){Id = Guid.NewGuid(), CourseCode = "Hist-400", CourseName = "Canada's History", Description = "A course to expand ones knowledge of the history of Canada", SchoolId = _schoolId},
            };

            //generate a new random db instance for testing purposes and to prevent test overlap
            var dbOptions = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _dbContext = new ApplicationContext(dbOptions);
            _dbContext.Courses.AddRange(_courseList);
            _dbContext.SaveChanges();
            _courseRepo = new CourseRepository(_dbContext);
        }

        [Fact]
        public async Task AddNewCourse_ReturnsNewCourse()
        {
            Course result = await _courseRepo.AddCourseAsync(new Course(){Id = Guid.Empty, CourseCode = "Rand-234", CourseName = "Random Course", Description = "A random and long description here", SchoolId = _schoolId});

            Assert.IsType<Course>(result);
            Assert.NotNull(result);
            Assert.Equal(5, _dbContext.Courses.Count());
        }

        [Fact]
        public async Task AddNewCourse_ThrowsErrorWithMatchingCourseCode()
        {
            await Assert.ThrowsAsync<ApiExceptions>(() => _courseRepo.AddCourseAsync(new Course(){Id = Guid.Empty, CourseCode = "Math-100", CourseName = "Random Course", Description = "A random and long description here", SchoolId = _schoolId}));
        }

        [Fact]
        public async Task GetCourseAsync_ReturnCourseWithMatchingId()
        {
            Course result = await _courseRepo.GetCourseAsync(_courseList.First().Id);

            Assert.IsType<Course>(result);
            Assert.NotNull(result);
            Assert.Equal(_courseList.First(), result);
        }

        [Fact]
        public async Task GetCourseAsync_ThrowsErrorWithNonMatchingId()
        {
            await Assert.ThrowsAsync<ApiExceptions>(() => _courseRepo.GetCourseAsync(Guid.NewGuid()));
        }

        [Fact]
        public async Task GetCourseListAsync_ReturnsSchoolCourseList()
        {
            var result = await _courseRepo.GetCourseListAsync(s => s.SchoolId == _schoolId, new RequestParameters(){PageNumber = 1, PageSize = 25});

            Assert.IsType<List<Course>>(result);
            Assert.NotEmpty(result);
            Assert.Equal(4, result.Count);
        }

        [Fact]
        public async Task GetCourseListAsync_ReturnsEmptyListWithInvalidPredicate()
        {
            var result = await _courseRepo.GetCourseListAsync(s => s.SchoolId == Guid.NewGuid(), new RequestParameters(){PageNumber = 1, PageSize = 25});

            Assert.IsType<List<Course>>(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetCourseListAsync_ReturnsCorrectPagedResults()
        {
            var result = await _courseRepo.GetCourseListAsync(s => s.SchoolId == _schoolId, new RequestParameters(){PageNumber = 1, PageSize = 2});

            Assert.IsType<List<Course>>(result);
            Assert.NotEmpty(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task DeleteCourseAsync_ReturnsTrue()
        {
            var result = await _courseRepo.DeleteCourseAsync(_courseList.First().Id);

            Assert.IsType<bool>(result);
            Assert.True(result);
            Assert.Equal(3, _dbContext.Courses.Count());
        }

        [Fact]
        public async Task DeleteCourseAsync_ThrowsErrorWhenCourseNotFound()
        {
            await Assert.ThrowsAsync<ApiExceptions>(() => _courseRepo.DeleteCourseAsync(Guid.NewGuid()));
        }

        [Fact]
        public async Task EditCourseAsync_ReturnsUpdatedCourse()
        {
            var updatedCourse = _courseList.Last();
            updatedCourse.CourseCode = "Trig-500";
            updatedCourse.CourseName = "Advanced Trigonometry";

            var result = await _courseRepo.EditCourseAsync(updatedCourse);

            Assert.IsType<Course>(result);
            Assert.NotNull(result);
            Assert.Equal(updatedCourse.CourseCode, result.CourseCode);
        }

        [Fact]
        public async Task EditCourseAsync_ThrowError()
        {
            var updatedCourse = _courseList.Last();
            updatedCourse.Id = Guid.Empty;
            updatedCourse.CourseCode = "Trig-500";
            updatedCourse.CourseName = "Advanced Trigonometry";

            await Assert.ThrowsAsync<ApiExceptions>(() => _courseRepo.EditCourseAsync(updatedCourse));
        }
    }
}