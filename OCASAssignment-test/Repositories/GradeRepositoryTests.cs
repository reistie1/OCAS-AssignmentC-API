using Microsoft.EntityFrameworkCore;
using OCAS.Domain.Common;
using OCASAPI.Application.Exceptions;
using OCASAPI.Infrastructure.Context;
using OCASAPI.Infrastructure.Repositories;

namespace OCASAPI_Tests.Repository
{
    public class GradeRepositoryTest
    {
        private ApplicationContext _dbContext;
        private GradeRepository _gradeRepo;
        private readonly List<Grade> _gradeList;
        private readonly List<Student> _studentList;
        private readonly List<Course> _courseList;
        private readonly Guid _schoolId;
        

        public GradeRepositoryTest()
        {
            _schoolId = Guid.NewGuid();

            _studentList = new List<Student>()
            {
                new Student(){Id = Guid.NewGuid(), FirstName = "Alex", LastName = "Jones", SchoolId = _schoolId},
                new Student(){Id = Guid.NewGuid(), FirstName = "John", LastName = "smith", SchoolId = _schoolId}
            };

            _courseList = new List<Course>()
            {
                new Course(){Id = Guid.NewGuid(), CourseCode = "Math-100", CourseName = "Basic Mathematics", Description = "A starters math course to further ones basic math skills", SchoolId = _schoolId},
                new Course(){Id = Guid.NewGuid(), CourseCode = "Science-200", CourseName = "Advanced Science", Description = "A challenging science course to further ones advanced science skills", SchoolId = _schoolId},
                new Course(){Id = Guid.NewGuid(), CourseCode = "Gym-321", CourseName = "Games Gym", Description = "A fun gym course to play games and further cardiovascular health", SchoolId = _schoolId},
                new Course(){Id = Guid.NewGuid(), CourseCode = "Hist-400", CourseName = "Canada's History", Description = "A course to expand ones knowledge of the history of Canada", SchoolId = _schoolId},
            };

            _gradeList = new List<Grade>()
            {
                new Grade(){Id = Guid.NewGuid(), AlphabeticGrade = 'A', NumericGrade = 89, StudentId = _studentList.First().Id, CourseId = _courseList.First().Id},
                new Grade(){Id = Guid.NewGuid(), AlphabeticGrade = 'B', NumericGrade = 75, StudentId = _studentList.First().Id, CourseId = _courseList.Last().Id},
                new Grade(){Id = Guid.NewGuid(), AlphabeticGrade = 'C', NumericGrade = 64, StudentId = _studentList.Last().Id, CourseId = _courseList.First().Id},
            };

            //generate a new random db instance for testing purposes and to prevent test overlap
            var dbOptions = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _dbContext = new ApplicationContext(dbOptions);
            _dbContext.Students.AddRange(_studentList);
            _dbContext.Courses.AddRange(_courseList);
            _dbContext.Grades.AddRange(_gradeList);
            _dbContext.SaveChanges();
            _gradeRepo = new GradeRepository(_dbContext);
        }

        [Fact]
        public async Task AddNewGrade_ReturnsNewGrade()
        {
            var result = await _gradeRepo.AddStudentGradeAsync(new Grade(){Id = Guid.NewGuid(), AlphabeticGrade = 'D', NumericGrade = 55, StudentId = _studentList.Last().Id});

            Assert.IsType<Grade>(result);
            Assert.NotNull(result);
            Assert.Equal(4, _dbContext.Grades.Count());
        }

        [Fact]
        public async Task AddNewGrade_ThrowsErrorWithMatchingGradeCode()
        {
            await Assert.ThrowsAsync<ApiExceptions>(() => _gradeRepo.AddStudentGradeAsync(_gradeList.First()));
        }

        [Fact]
        public async Task GetStudentCourseGradeAsync_ReturnGradeWithMatchingId()
        {
            var grade = _gradeList.First();
            var result = await _gradeRepo.GetStudentCourseGradeAsync(grade.StudentId, grade.CourseId);

            Assert.IsType<Grade>(result);
            Assert.NotNull(result);
            Assert.Equal(_gradeList.First(), result);
        }

        [Fact]
        public async Task GetStudentCourseGradeAsync_ThrowsErrorWithInvalidIds()
        {
            await Assert.ThrowsAsync<ApiExceptions>(() => _gradeRepo.GetStudentCourseGradeAsync(Guid.NewGuid(), Guid.NewGuid()));
        }

        [Fact]
        public async Task GetStudentGradesAsync_ReturnsStudentGradeList()
        {
            var result = await _gradeRepo.GetStudentGradesAsync(_studentList.First().Id);

            Assert.IsType<List<Grade>>(result);
            Assert.NotEmpty(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task GetStudentGradesAsync_ReturnsEmptyListWithInvalidId()
        {
            var result = await _gradeRepo.GetStudentGradesAsync(Guid.Empty);

            Assert.IsType<List<Grade>>(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task DeleteGradeAsync_ReturnsTrue()
        {
            var grade = _gradeList.First();
            var result = await _gradeRepo.DeleteStudentGradeAsync(grade.StudentId, grade.CourseId);

            Assert.IsType<bool>(result);
            Assert.True(result);
            Assert.Equal(2, _dbContext.Grades.Count());
        }

        [Fact]
        public async Task DeleteCourseAsync_ThrowsErrorWhenCourseNotFound()
        {
            await Assert.ThrowsAsync<ApiExceptions>(() => _gradeRepo.DeleteStudentGradeAsync(Guid.NewGuid(), Guid.NewGuid()));
        }

        [Fact]
        public async Task UpdateStudentGradeAsync_ReturnsUpdatedStudentGrade()
        {
            var updatedGrade = _gradeList.Last();
            updatedGrade.AlphabeticGrade = 'B';
            updatedGrade.NumericGrade = 70;

            var result = await _gradeRepo.UpdateStudentGradeAsync(_gradeList.Last());

            Assert.IsType<Grade>(result);
            Assert.NotNull(result);
            Assert.Equal(updatedGrade.NumericGrade, result.NumericGrade);
            Assert.Equal(updatedGrade.AlphabeticGrade, result.AlphabeticGrade);
        }

        [Fact]
        public async Task EditCourseAsync_ThrowError()
        {
            var updatedGrade = _gradeList.Last();
            updatedGrade.CourseId = Guid.NewGuid();
            updatedGrade.AlphabeticGrade = 'B';
            updatedGrade.NumericGrade = 70;

            await Assert.ThrowsAsync<ApiExceptions>(() => _gradeRepo.UpdateStudentGradeAsync(updatedGrade));
        }
    }
}