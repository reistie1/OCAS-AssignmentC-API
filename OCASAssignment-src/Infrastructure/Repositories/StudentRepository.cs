using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using OCAS.Domain.Common;
using OCASAPI.Application.Exceptions;
using OCASAPI.Infrastructure.Context;

namespace OCASAPI.Infrastructure.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationContext _context;
        private readonly DbSet<Student> _students;
        
        public StudentRepository(ApplicationContext context)
        {
            _students = context.Set<Student>();
            _context = context;
        }

        public async Task<bool> AddCourseAsync(Guid StudentId, Guid CourseId)
        {
            var existing = await _students.Where(s => s.Id == StudentId).Include(s => s.Courses).FirstOrDefaultAsync();

            if(existing == null)
            {
                throw new ApiExceptions("Student not found");
            }
            else
            {
                if(existing.Courses.Any(c => c.Id == CourseId))
                {
                    throw new ApiExceptions("Student is already enrolled in that course");
                }
                else
                {
                    existing.Courses.Add(await _context.Courses.Where(c => c.Id == CourseId).FirstOrDefaultAsync());

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
            }
        }

        public async Task<Student> AddStudentAsync(Student student)
        {
            var existing = await _students.Where(s => s.Id == student.Id).FirstOrDefaultAsync();

            if(existing != null)
            {
                throw new ApiExceptions("Student not found");
            }
            else
            {
                var newStudent = await _students.AddAsync(student);
                var result = await _context.SaveChangesAsync();

                if(result > 0)
                {
                    return newStudent.Entity;
                }
                else
                {
                    throw new ApiExceptions("Error adding student");
                }
            }
        }

        public async Task<IReadOnlyList<Student>> GetStudentCoursesAsync(Expression<Func<Student, bool>> predicate)
        {
            return await _students.Where(predicate)
                .Include(c => c.Courses)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<bool> RemoveCourseAsync(Guid StudentId, Guid CourseId)
        {
            var existing = await _students.Where(s => s.Id == StudentId).Include(c => c.Courses).FirstOrDefaultAsync();

            if(existing == null)
            {
                throw new ApiExceptions("Student not found");
            }
            else
            {
                if(!existing.Courses.Any(c => c.Id == CourseId))
                {
                    throw new ApiExceptions("Student not part of this course");
                } 
                else
                {
                    existing.Courses.Remove(await _context.Courses.Where(c => c.Id == CourseId).FirstOrDefaultAsync());

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
            }
        }
    }
}