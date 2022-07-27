using System.Linq.Expressions;
using OCAS.Domain.Common;
using OCASAPI.Application.DTO.Requests;

public interface ITeacherRepository
{
    Task<bool> AddCourseTeacherAsync(Guid CourseId, Guid TeacherId);
    Task<Teacher> AddSchoolTeacherAsync(Teacher teacher);
    Task<bool> ChangeCourseTeacherAsync(ChangeTeacherRequest ChangeTeacher);
    Task<Teacher> UpdateTeacherAsync(Teacher teacher);
    Task<bool> RemoveCourseTeacherAsync(Guid TeacherId, Guid CourseId);
    Task<bool> DeleteTeacherAsync(Guid teacherId);
    Task<IReadOnlyList<Teacher>> GetTeacherCoursesAsync(Expression<Func<Teacher, bool>> predicate);
}