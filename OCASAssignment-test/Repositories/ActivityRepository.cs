using Microsoft.EntityFrameworkCore;
using OCAS.Domain.Common;
using OCASAPI.Application.Exceptions;
using OCASAPI.Infrastructure.Context;
using OCASAPI.Infrastructure.Repositories;

namespace ActivityRepositoryTests
{
    public class ActivityRepositoryTests
    {
        private ApplicationContext _dbContext;
        private ActivityRepository _activityRepo;
        private List<Activity> _activityList;
        private List<ActivitySignUp> _signUpList;

        public ActivityRepositoryTests()
        {
            _activityList = new List<Activity>()
            {
                new Activity(){Id = Guid.NewGuid(), ActivityName = "Floor Hockey", Description = "A nice friendly game of floor hockey between employees"},
                new Activity(){Id = Guid.NewGuid(), ActivityName = "Three Pitch", Description = "A nice friendly game of baseball between departments"},
                new Activity(){Id = Guid.NewGuid(), ActivityName = "Basketball", Description = "A nice friendly game of basketball between employees"},
                new Activity(){Id = Guid.NewGuid(), ActivityName = "Badminton", Description = "A nice friendly game of badminton between employees"},
            };

            _signUpList = new List<ActivitySignUp>()
            {
                new ActivitySignUp(){Id = Guid.NewGuid(), FirstName = "John", LastName = "Snow", ActivityId = _activityList.First().Id, Email = "test1@gmail.com"},
                new ActivitySignUp(){Id = Guid.NewGuid(), FirstName = "Scott", LastName = "Pilgram", ActivityId = _activityList.First().Id, Email = "test2@gmail.com"},
                new ActivitySignUp(){Id = Guid.NewGuid(), FirstName = "Adam", LastName = "Scott", ActivityId = _activityList.Last().Id, Email = "test3@gmail.com"},
            };


            //generate a new random db instance for testing purposes and to prevent test overlap
            var dbOptions = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _dbContext = new ApplicationContext(dbOptions);
            _dbContext.AddRange(_activityList);
            _dbContext.AddRange(_signUpList);
            _dbContext.SaveChanges();
            _activityRepo = new ActivityRepository(_dbContext);
        }

        [Fact]
        public async Task GetActivityList_ReturnsActivityList()
        {
            var list = await _activityRepo.GetActivityListAsync();

            Assert.NotEmpty(list);
            Assert.IsType<List<Activity>>(list);
            Assert.Equal(4, _dbContext.Activities.Count());
        }

        [Fact]
        public async Task GetPeopleEnrolledInActivity_ReturnsEnrolledActivityList()
        {
            var list = await _activityRepo.GetPeopleEnrolledInActivity(_activityList.First().Id);

            Assert.NotEmpty(list);
            Assert.IsType<List<ActivitySignUp>>(list);
            Assert.Equal(2, list.Count);
        }

        [Fact]
        public async Task GetPeopleEnrolledInActivity_ReturnsEmptyList()
        {
            var list = await _activityRepo.GetPeopleEnrolledInActivity(Guid.NewGuid());

            Assert.Empty(list);
            Assert.IsType<List<ActivitySignUp>>(list);
        }

        [Fact]
        public async Task AddPersonToActivityAsync_ReturnsEnrolledEntityList()
        {
            var result = await _activityRepo.AddPersonToActivityAsync(new ActivitySignUp(){
                Id = Guid.NewGuid(),
                FirstName = "Bob",
                LastName = "Johnson",
                Email = "test5@gmail.com",
                ActivityId = _activityList.Last().Id
            });

            Assert.True(result);
            Assert.IsType<bool>(result);
        }

        public async Task AddPersonToActivityAsync_ThrowsError()
        {
            await Assert.ThrowsAsync<ApiExceptions>(() => _activityRepo.AddPersonToActivityAsync(_signUpList.First()));
        }

    
       
    }
}