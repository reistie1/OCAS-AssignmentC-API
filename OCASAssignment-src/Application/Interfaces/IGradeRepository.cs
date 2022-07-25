using OCAS.Domain.Common;

public interface IGradeRepository
{
    Task<Grade> AddStudentGradeAsync(Guid StudentId, Grade grade);
    Task<Grade> UpdateStudentGradeAsync(Guid StudentId, Grade grade);
    Task<bool> DeleteStudentGradeAsync(Guid StudentId, Guid CourseId);
    Task<Grade> GetStudentCourseGradeAsync(Guid StudentId, Guid CourseId);
    Task<IReadOnlyList<Grade>> GetStudentGradesAsync(Guid StudentId);
}