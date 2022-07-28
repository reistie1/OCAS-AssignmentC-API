using System.Linq.Expressions;
using OCAS.Domain.Common;

public interface IStudentRepository
{
    Task<bool> AddCourseAsync(Guid StudentId, Guid CourseId);
    Task<Student> AddStudentAsync(Student student);
    Task<Student> UpdateStudentAsync(Student student);
    Task<Student> GetStudentAsync(Guid StudentId);
    Task<bool> RemoveCourseAsync(Guid StudentId, Guid CourseId);
    Task<bool> DeleteStudentAsync(Guid StudentId);
    Task<IReadOnlyList<Student>> GetSchoolStudentsAsync(Expression<Func<Student, bool>> predicate);

}