using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using OCAS.Domain.Common;
using OCASAPI.Application.Exceptions;
using OCASAPI.Application.Parameters;
using OCASAPI.Infrastructure.Context;

namespace OCASAPI.Infrastructure.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly DbSet<Course> _courses;
        private readonly ApplicationContext _context;
        public CourseRepository(ApplicationContext context)
        {
            _context = context;
            _courses = context.Set<Course>();
        }

        public async Task<Course> AddCourseAsync(Course course)
        {
            var existing = await _courses.Where(c => c.Id == course.Id).FirstOrDefaultAsync();

            if(existing != null)
            {
                throw new ApiExceptions("Course already exists");
            }

            var newCourse = await _courses.AddAsync(course);
            var result = await _context.SaveChangesAsync();

            if(result > 0)
            {
                return newCourse.Entity;
            }
            else
            {
                throw new ApiExceptions("Error saving course");
            }
        }

        public async Task<bool> DeleteCourseAsync(Guid courseId)
        {
            var existing = await _courses.Where(c => c.Id == courseId).FirstOrDefaultAsync();

            if(existing == null)
            {
                throw new ApiExceptions("Course not found");
            }
            else
            {
                _courses.Remove(existing);
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

        public async Task<Course> EditCourseAsync(Course course)
        {
            var existing = await _courses.Where(c => c.Id == course.Id).FirstOrDefaultAsync();

            if(existing == null)
            {
                throw new ApiExceptions("Course not found");
            }

            _context.Entry(existing).CurrentValues.SetValues(new {
                CourseName = course.CourseName,
                CourseCode = course.CourseCode,
                Description = course.Description,
                TeacherId = course.TeacherId,
                SchoolId = course.SchoolId
            });

            var result = await _context.SaveChangesAsync();

            if(result > 0)
            {
                return await this.GetCourseAsync(course.Id);
            }
            else
            {
                throw new ApiExceptions("Error saving course");
            }
            
        }

        public async Task<Course> GetCourseAsync(Guid CourseId)
        {
            var existing =  await _courses.Where(c => c.Id == CourseId).FirstOrDefaultAsync();

            if(existing == null)
            {
                throw new ApiExceptions("Course not found");
            }
            else
            {
                return existing;
            }
        }

        public async Task<IReadOnlyList<Course>> GetCourseListAsync(Expression<Func<Course, bool>> predicate, RequestParameters requestParams)
        {
           return await _courses.Where(predicate)
                .Skip((requestParams.PageNumber - 1) * requestParams.PageSize)
                .Take(requestParams.PageSize)
                .OrderBy(c => c.CourseCode)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}