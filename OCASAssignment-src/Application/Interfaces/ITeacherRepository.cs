using System.Linq.Expressions;
using OCAS.Domain.Common;
using OCASAPI.Application.DTO.Requests;

public interface ITeacherRepository
{
    Task<bool> AddCourseTeacherAsync(Guid CourseId, Guid TeacherId);
    Task<bool> ChangeCourseTeacherAsync(ChangeTeacherRequest ChangeTeacher);
    Task<bool> RemoveCourseTeacherAsync(Guid TeacherId, Guid CourseId);
    Task<IReadOnlyList<Teacher>> GetTeacherCoursesAsync(Expression<Func<Teacher, bool>> predicate);
}