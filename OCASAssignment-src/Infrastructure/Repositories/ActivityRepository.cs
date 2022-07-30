using Microsoft.EntityFrameworkCore;
using OCAS.Domain.Common;
using OCASAPI.Application.Exceptions;
using OCASAPI.Application.Interfaces;
using OCASAPI.Application.Parameters;
using OCASAPI.Infrastructure.Context;

namespace OCASAPI.Infrastructure.Repositories
{
    public class ActivityRepository : IActivityRepository
    {
        private readonly ApplicationContext _context;
        private readonly DbSet<Activity> _activities;
        private readonly DbSet<ActivitySignUp> _activityList;

        public ActivityRepository(ApplicationContext context)
        {
            _context = context;
            _activities = context.Set<Activity>();
            _activityList = context.Set<ActivitySignUp>();
        }


        public async Task<bool> AddPersonToActivityAsync(ActivitySignUp person)
        {
            var existing = await _activityList.Where(a => a.ActivityId == person.ActivityId && a.Email == person.Email).FirstOrDefaultAsync();

            if(existing != null)
            {
                throw new ApiExceptions("Person with that email is already enrolled in this activity");
            }
            else
            {
                await _activityList.AddAsync(person);
                var result = await _context.SaveChangesAsync();

                if(result == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public async Task<IReadOnlyList<Activity>> GetActivityListAsync()
        {
            return await _activities.AsNoTracking().ToListAsync();
        }

        public async Task<IReadOnlyList<ActivitySignUp>> GetPeopleEnrolledInActivity(Guid ActivityId, RequestParameters requestParams)
        {
            return await _activityList.Where(a => a.ActivityId == ActivityId)
            .Skip((requestParams.PageNumber - 1) * requestParams.PageSize)
            .Take(requestParams.PageSize)
            .AsNoTracking()
            .ToListAsync();
        }
    }
}