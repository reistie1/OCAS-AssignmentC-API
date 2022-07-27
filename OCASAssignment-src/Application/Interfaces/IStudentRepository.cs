using System.Linq.Expressions;
using OCAS.Domain.Common;

public interface IStudentRepository
{
    Task<bool> AddCourseAsync(Guid StudentId, Guid CourseId);
    Task<Student> AddStudentAsync(Student student);
    Task<bool> RemoveCourseAsync(Guid StudentId, Guid CourseId);
    Task<IReadOnlyList<Student>> GetStudentCoursesAsync(Expression<Func<Student, bool>> predicate);
}