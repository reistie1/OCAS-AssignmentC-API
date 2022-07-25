using Microsoft.EntityFrameworkCore;
using OCAS.Domain.Common;
using OCASAPI.Application.Exceptions;
using OCASAPI.Infrastructure.Context;

namespace OCASAPI.Infrastructure.Repositories
{
    public class GradeRepository : IGradeRepository
    {
        private readonly ApplicationContext _context;
        private readonly DbSet<Grade> _grades;
        public GradeRepository(ApplicationContext context)
        {
            _context = context;
            _grades = context.Set<Grade>();
        }


        public async Task<Grade> AddStudentGradeAsync(Guid StudentId, Grade grade)
        {
            var existing = await _grades.Where(c => c.StudentId == StudentId).FirstOrDefaultAsync();

            if(existing != null)
            {
                throw new ApiExceptions("Grade for this course already exists for this student");
            }

            var newGrade = await _grades.AddAsync(grade);
            var result = await _context.SaveChangesAsync();

            if(result > 0)
            {
                return newGrade.Entity;
            }
            else
            {
                throw new ApiExceptions("Error saving grade");
            }
        }

        public async Task<bool> DeleteStudentGradeAsync(Guid StudentId, Guid CourseId)
        {
            var existing = await _grades.Where(c => c.Id == CourseId && c.StudentId == StudentId).FirstOrDefaultAsync();

            if(existing == null)
            {
                throw new ApiExceptions("Course not found");
            }
            else
            {
                _grades.Remove(existing);
            }

            var result = await _context.SaveChangesAsync();

            if(result == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<Grade> GetStudentCourseGradeAsync(Guid StudentId, Guid CourseId)
        {
            var existing =  await _grades.Where(c => c.StudentId == StudentId && c.CourseId == CourseId).FirstOrDefaultAsync();

            if(existing == null)
            {
                throw new ApiExceptions("Course not found");
            }
            else
            {
                return existing;
            }
        }

        public async Task<IReadOnlyList<Grade>> GetStudentGradesAsync(Guid StudentId)
        {
            return await _grades.Where(g => g.StudentId == StudentId)
                .Include(c => c.Course)
                .OrderBy(c => c.Course.CourseCode)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Grade> UpdateStudentGradeAsync(Guid StudentId, Grade grade)
        {
            var existing = await _grades.Where(c => c.StudentId == StudentId).FirstOrDefaultAsync();

            if(existing == null)
            {
                throw new ApiExceptions("Student grade not found");
            }

            _context.Entry(existing).CurrentValues.SetValues(new {
                NumericGrade = grade.NumericGrade,
                Alphabetic = grade.AlphabeticGrade,
            });

            var result = await _context.SaveChangesAsync();

            if(result > 0)
            {
                return await this.GetStudentCourseGradeAsync(StudentId, grade.CourseId);
            }
            else
            {
                throw new ApiExceptions("Error saving course");
            }
        }
    }
}