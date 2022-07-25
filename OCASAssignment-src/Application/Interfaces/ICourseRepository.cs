using System.Linq.Expressions;
using OCAS.Domain.Common;
using OCASAPI.Application.DTO.Common;
using OCASAPI.Application.Parameters;

public interface ICourseRepository
{
    Task<CourseDto> AddCourseAsync(Course course);
    Task<CourseDto> EditCourseAsync(Course course);
    Task<bool> DeleteCourseAsync(Guid courseId);
    Task<IReadOnlyList<CourseDto>> GetCourseListAsync(Expression<Func<Course, bool>> predicate, RequestParameters requestParams);

}