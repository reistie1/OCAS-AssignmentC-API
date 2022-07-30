using OCAS.Domain.Common;
using OCASAPI.Application.Parameters;

namespace OCASAPI.Application.Interfaces
{
    public interface IActivityRepository
    {
        Task<bool> AddPersonToActivityAsync(ActivitySignUp person);
        Task<IReadOnlyList<Activity>> GetActivityListAsync();
        Task<IReadOnlyList<ActivitySignUp>> GetPeopleEnrolledInActivity(Guid ActivityId, RequestParameters requestParams);
    }
}