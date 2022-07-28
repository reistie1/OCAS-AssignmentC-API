using Microsoft.EntityFrameworkCore;
using OCAS.Domain.Common;
using OCASAPI.Application.Exceptions;
using OCASAPI.Application.Parameters;
using OCASAPI.Infrastructure.Context;
using OCASAPI.Infrastructure.Repositories;

namespace OCASAPI_Tests.Repository
{
    public class StudentRepositoryTest
    {
        private ApplicationContext _dbContext;
        private StudentRepository _studentRepo;
        private readonly List<Student> _studentList;
        private readonly List<Course> _courseList;
        private readonly Guid _schoolId;
        

        public StudentRepositoryTest()
        {
            _schoolId = Guid.NewGuid();

            _courseList = new List<Course>()
            {
                new Course(){Id = Guid.NewGuid(), CourseCode = "Math-100", CourseName = "Basic Mathematics", Description = "A starters math course to further ones basic math skills", SchoolId = _schoolId},
                new Course(){Id = Guid.NewGuid(), CourseCode = "Science-200", CourseName = "Advanced Science", Description = "A challenging science course to further ones advanced science skills", SchoolId = _schoolId},
                new Course(){Id = Guid.NewGuid(), CourseCode = "Gym-321", CourseName = "Games Gym", Description = "A fun gym course to play games and further cardiovascular health", SchoolId = _schoolId},
                new Course(){Id = Guid.NewGuid(), CourseCode = "Hist-400", CourseName = "Canada's History", Description = "A course to expand ones knowledge of the history of Canada", SchoolId = _schoolId},
            };

            _studentList = new List<Student>()
            {
                new Student(){Id = Guid.NewGuid(), FirstName = "Alex", LastName = "Jones", SchoolId = _schoolId, Age = 15},
                new Student(){Id = Guid.NewGuid(), FirstName = "John", LastName = "smith", SchoolId = _schoolId, Age = 16}
            };

            //generate a new random db instance for testing purposes and to prevent test overlap
            var dbOptions = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _dbContext = new ApplicationContext(dbOptions);
            _dbContext.Courses.AddRange(_courseList);
            _dbContext.Students.AddRange(_studentList);
            _dbContext.SaveChanges();
            _studentRepo = new StudentRepository(_dbContext);
        }

        [Fact]
        public async Task AddCourseAsync_ReturnsTrue()
        {
            var result = await _studentRepo.AddCourseAsync(_studentList.First().Id, _courseList.First().Id);

            Assert.IsType<bool>(result);
            Assert.True(result);
        }

        [Fact]
        public async Task AddCourseAsync_ThrowsErrorWithInvalidIdsForCourseAndStudent()
        {
            await Assert.ThrowsAsync<ApiExceptions>(() => _studentRepo.AddCourseAsync(Guid.NewGuid(), Guid.NewGuid()));
        }

        [Fact]
        public async Task AddCourseAsync_ThrowsErrorWithDuplicateCourseForStudent()
        {
            var result = await _studentRepo.AddCourseAsync(_studentList.First().Id, _courseList.First().Id);

            await Assert.ThrowsAsync<ApiExceptions>(() => _studentRepo.AddCourseAsync(_studentList.First().Id, _courseList.First().Id));

        }

        [Fact]
        public async Task AddStudentAsync_ReturnsAddedStudent()
        {
            var result = await _studentRepo.AddStudentAsync(new Student(){Id = Guid.NewGuid(), FirstName = "Roger", LastName = "Rabbit", Age = 21, SchoolId = _schoolId});

            Assert.IsType<Student>(result);
            Assert.NotNull(result);
            Assert.Equal(3, _dbContext.Students.Count());
        }

        [Fact]
        public async Task AddStudentAsync_ThrowErrorWithSameId()
        {
            var result = await _studentRepo.AddStudentAsync(new Student(){Id = Guid.NewGuid(), FirstName = "Roger", LastName = "Rabbit", Age = 21, SchoolId = _schoolId});

            await Assert.ThrowsAsync<ApiExceptions>(() => _studentRepo.AddStudentAsync(_studentList.First()));
        }

        [Fact]
        public async Task GetStudentAsync_ReturnCourseWithMatchingId()
        {
            var result = await _studentRepo.GetStudentAsync(_studentList.First().Id);

            Assert.IsType<Student>(result);
            Assert.NotNull(result);
            Assert.Equal(_studentList.First(), result);
        }

        [Fact]
        public async Task GetStudentAsync_ThrowsError()
        {
            await Assert.ThrowsAsync<ApiExceptions>(() => _studentRepo.GetStudentAsync(Guid.NewGuid()));
        }

        [Fact]
        public async Task GetSchoolStudentsAsync_ReturnsSchoolStudentList()
        {
            var result = await _studentRepo.GetSchoolStudentsAsync(s => s.SchoolId == _schoolId);

            Assert.IsType<List<Student>>(result);
            Assert.NotEmpty(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task GetSchoolStudentsAsync_ReturnsEmptyListWithInvalidPredicate()
        {
            var result = await _studentRepo.GetSchoolStudentsAsync(s => s.SchoolId == Guid.NewGuid());

            Assert.IsType<List<Student>>(result);
            Assert.Empty(result);
        }


        [Fact]
        public async Task DeleteStudentAsync_ReturnsTrue()
        {
            var result = await _studentRepo.DeleteStudentAsync(_studentList.First().Id);

            Assert.IsType<bool>(result);
            Assert.True(result);
            Assert.Equal(1, _dbContext.Students.Count());
        }

        [Fact]
        public async Task DeleteStudentAsync_ThrowsErrorWhenCourseNotFound()
        {
            await Assert.ThrowsAsync<ApiExceptions>(() => _studentRepo.DeleteStudentAsync(Guid.NewGuid()));
        }

        [Fact]
        public async Task RemoveCourseAsync_ReturnsTrue()
        {
            await _studentRepo.AddCourseAsync(_studentList.First().Id, _courseList.First().Id);
            var result = await _studentRepo.RemoveCourseAsync(_studentList.First().Id, _courseList.First().Id);

            Assert.IsType<bool>(result);
            Assert.True(result);
        }

        [Fact]
        public async Task RemoveCourseAsync_ThrowsErrorWhenStudentNotPartOfCourse()
        {
            await _studentRepo.AddCourseAsync(_studentList.First().Id, _courseList.First().Id);
            var result = await _studentRepo.RemoveCourseAsync(_studentList.First().Id, _courseList.First().Id);

            await Assert.ThrowsAsync<ApiExceptions>(() => _studentRepo.RemoveCourseAsync(_studentList.First().Id, _courseList.Last().Id));

        }

        [Fact]
        public async Task RemoveCourseAsync_ThrowsErrorWhenCourseNotFound()
        {
            await Assert.ThrowsAsync<ApiExceptions>(() => _studentRepo.RemoveCourseAsync(Guid.NewGuid(), Guid.NewGuid()));
        }

        [Fact]
        public async Task UpdateStudentAsync_ReturnsUpdatedStudentEntity()
        {
            var updatedStudent = _studentList.Last();
            updatedStudent.FirstName = "Josh";
            updatedStudent.LastName = "Reist";

            var result = await _studentRepo.UpdateStudentAsync(updatedStudent);

            Assert.IsType<Student>(result);
            Assert.NotNull(result);
            Assert.Equal(updatedStudent, result);
        }

        [Fact]
        public async Task UpdateStudentAsync_ThrowsError()
        {
            var updatedStudent = _studentList.Last();
            updatedStudent.Id = Guid.NewGuid();
            updatedStudent.FirstName = "Josh";
            updatedStudent.LastName = "Reist";

            await Assert.ThrowsAsync<ApiExceptions>(() => _studentRepo.UpdateStudentAsync(updatedStudent));
        }
    }
}