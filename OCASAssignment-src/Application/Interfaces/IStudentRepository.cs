using System.Linq.Expressions;
using OCAS.Domain.Common;

public interface IStudentRepository
{
    Task<bool> AddCourseAsync(Guid StudentId, Guid CourseId);
    Task<Student> AddStudentAsync(Student student);
    Task<Student> UpdateStudentAsync(Student student);
    Task<IReadOnlyList<Student>> GetSchoolStudents(Guid SchoolId);
    Task<bool> RemoveCourseAsync(Guid StudentId, Guid CourseId);
    Task<bool> DeleteStudentAsync(Guid StudentId);
    Task<IReadOnlyList<Student>> GetStudentCoursesAsync(Expression<Func<Student, bool>> predicate);
}