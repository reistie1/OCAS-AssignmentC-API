using OCAS.Domain.Common;

public interface ISchoolRepository
{
    Task<School> GetSchoolInformationAsync(Guid SchoolId);
    Task<School> UpdateSchoolInformationAsync(School school);
}