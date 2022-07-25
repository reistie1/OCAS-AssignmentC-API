using System.Linq.Expressions;
using OCAS.Domain.Common;
using OCASAPI.Application.DTO.Common;
using OCASAPI.Application.Parameters;

public interface ICourseRepository
{
    Task<Course> AddCourseAsync(Course course);
    Task<Course> EditCourseAsync(Course course);
    Task<bool> DeleteCourseAsync(Guid courseId);
    Task<IReadOnlyList<Course>> GetCourseListAsync(Expression<Func<Course, bool>> predicate, RequestParameters requestParams);
    Task<Course> GetCourseAsync(Guid CourseId);

}