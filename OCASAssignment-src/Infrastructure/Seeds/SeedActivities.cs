using OCAS.Domain.Common;
using OCASAPI.Infrastructure.Context;

namespace OCASAPI.Infrastructure.Seeds
{
    public class SeedActivities
    {
        private ApplicationContext _context;
        public SeedActivities(ApplicationContext context)
        {
            _context = context;
        }

        public List<Activity> createList()
        {
            List<Activity> Activities = new List<Activity>()
            {
                new Activity(){Id = Guid.NewGuid(), ActivityName = "Floor Hockey", Description = "A nice friendly game of floor hockey between employees"},
                new Activity(){Id = Guid.NewGuid(), ActivityName = "Three Pitch", Description = "A nice friendly game of baseball between departments"},
                new Activity(){Id = Guid.NewGuid(), ActivityName = "Basketball", Description = "A nice friendly game of basketball between employees"},
                new Activity(){Id = Guid.NewGuid(), ActivityName = "Badminton", Description = "A nice friendly game of badminton between employees"},
                new Activity(){Id = Guid.NewGuid(), ActivityName = "Tennis", Description = "A nice friendly game of tennis between employees"},
                new Activity(){Id = Guid.NewGuid(), ActivityName = "Football", Description = "A nice friendly game of football between employees"},
                new Activity(){Id = Guid.NewGuid(), ActivityName = "Cricket", Description = "A nice friendly game of cricket between employees"},
            };

            return Activities;
        }
    }
}