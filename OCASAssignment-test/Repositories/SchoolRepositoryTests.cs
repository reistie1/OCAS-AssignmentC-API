using Microsoft.EntityFrameworkCore;
using OCAS.Domain.Common;
using OCASAPI.Application.Exceptions;
using OCASAPI.Infrastructure.Context;
using OCASAPI.Infrastructure.Repositories;

namespace OCASAPI_Tests.Repository
{
    public class SchoolRepositoryTest
    {
        private ApplicationContext _dbContext;
        private SchoolRepository _schoolRepo;
        private readonly School _school;
        private readonly Guid _schoolId;
        

        public SchoolRepositoryTest()
        {
            _schoolId = Guid.NewGuid();
            _school = new School()
            {
                Id = _schoolId,
                Name = "St.Paul's School",
                Address = new Address()
                {
                    Id = Guid.NewGuid(),
                    Address1 = "21 Random St W",
                    Address2 = null,
                    City = "Arlington",
                    Province = "Ontario",
                    PostalCode = "N4M 9S5"
                }
            };

            //generate a new random db instance for testing purposes and to prevent test overlap
            var dbOptions = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _dbContext = new ApplicationContext(dbOptions);
            _dbContext.Schools.Add(_school);
            _dbContext.SaveChanges();
            _schoolRepo = new SchoolRepository(_dbContext);
        }

        [Fact]
        public async Task GetSchoolInformationAsync_ReturnSchoolEntity()
        {
            var result = await _schoolRepo.GetSchoolInformationAsync(_schoolId);

            Assert.IsType<School>(result);
            Assert.NotNull(result);
            Assert.NotNull(result.Address);
            Assert.Equal(_school, result);
        }

        [Fact]
        public async Task GetSchoolInformationAsync_ThrowsErrorWithInvalidId()
        {
            await Assert.ThrowsAsync<ApiExceptions>(() => _schoolRepo.GetSchoolInformationAsync(Guid.NewGuid()));
        }

        [Fact]
        public async Task UpdateSchoolInformationAsync_ReturnsUpdatedSchoolInformation()
        {
            var school = _school;
            school.Name = "St.Jerome's";
            school.Address.Address1 = "56 Test Rd W";
            school.Address.Province = "Alberta";
            
            var result = await _schoolRepo.UpdateSchoolInformationAsync(school);

            Assert.IsType<School>(result);
            Assert.NotNull(result);
            Assert.Equal(school, result);
        }

        [Fact]
        public async Task GetStudentCourseGradeAsync_ThrowsErrorWithInvalidIds()
        {
            var school = _school;
            school.Id = Guid.Empty;

            await Assert.ThrowsAsync<ApiExceptions>(() => _schoolRepo.UpdateSchoolInformationAsync(school));
        }

        
    }
}