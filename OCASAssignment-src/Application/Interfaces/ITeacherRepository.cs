using System.Linq.Expressions;
using OCAS.Domain.Common;
using OCASAPI.Application.DTO.Requests;

public interface ITeacherRepository
{
    Task<bool> AddCourseTeacherAsync(Guid CourseId, Guid TeacherId);
    Task<Teacher> AddSchoolTeacherAsync(Teacher teacher);
    Task<Teacher> UpdateTeacherAsync(Teacher teacher);
    Task<bool> ChangeCourseTeacherAsync(ChangeTeacherRequest ChangeTeacher);
    Task<bool> RemoveCourseTeacherAsync(Guid TeacherId, Guid CourseId);
    Task<bool> DeleteTeacherAsync(Guid teacherId);
    Task<Teacher> GetTeacherAsync(Guid TeacherId);
    Task<IReadOnlyList<Teacher>> GetSchoolTeachersAsync(Expression<Func<Teacher, bool>> predicate);
}