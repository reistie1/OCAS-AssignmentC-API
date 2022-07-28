using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using OCAS.Domain.Common;
using OCASAPI.Application.DTO.Requests;
using OCASAPI.Application.Exceptions;
using OCASAPI.Infrastructure.Context;

namespace OCASAPI.Infrastructure.Repositories
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly ApplicationContext _context;
        private readonly DbSet<Teacher> _teachers;
        public TeacherRepository(ApplicationContext context)
        {
            _context = context;
            _teachers = context.Set<Teacher>();
        }

        public async Task<bool> AddCourseTeacherAsync(Guid CourseId, Guid TeacherId)
        {
            var existing = await _teachers.Where(s => s.Id == TeacherId).Include(s => s.Courses).FirstOrDefaultAsync();

            if(existing == null)
            {
                throw new ApiExceptions("Teacher not found");
            }
            else
            {
                if(existing.Courses.Any(c => c.Id == CourseId))
                {
                    throw new ApiExceptions("Teacher is already teaching this course");
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

        public async Task<Teacher> AddSchoolTeacherAsync(Teacher teacher)
        {
            var existing = await _teachers.Where(t => t.FirstName == teacher.FirstName && t.LastName == teacher.LastName).FirstOrDefaultAsync();

            if(existing != null)
            {
                throw new ApiExceptions("A teacher with that name already exists");
            }
            else
            {
                var addedTeacher = await _teachers.AddAsync(teacher);
                var result = await _context.SaveChangesAsync();

                if(result > 0)
                {
                    return addedTeacher.Entity;
                }
                else
                {
                    throw new ApiExceptions("Error saving teacher");
                }
            }
        }

        public async Task<bool> ChangeCourseTeacherAsync(ChangeTeacherRequest ChangeTeacher)
        {
            var existing = await _context.Courses.Where(s => s.Id == ChangeTeacher.CourseId).FirstOrDefaultAsync();

            if(existing == null)
            {
                throw new ApiExceptions("Course not found");
            }
            else
            {
                _context.Entry(existing).CurrentValues.SetValues(new {
                    TeacherId = ChangeTeacher.NewTeacherId
                });

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

        public async Task<bool> DeleteTeacherAsync(Guid teacherId)
        {
            var existing = await _teachers.Where(c => c.Id == teacherId).FirstOrDefaultAsync();

            if(existing == null)
            {
                throw new ApiExceptions("Teacher not found");
            }
            else
            {
                _teachers.Remove(existing);
            }

            var result = await _context.SaveChangesAsync();
            return true;

            // if(result == 0)
            // {
            //     return true;
            // }
            // else
            // {
            //     return false;
            // }
        }

        public async Task<IReadOnlyList<Teacher>> GetSchoolTeachersAsync(Expression<Func<Teacher, bool>> predicate)
        {
            return await _teachers.Where(predicate).Include(c => c.Courses).AsNoTracking().ToListAsync();
        }

        public async Task<Teacher> GetTeacherAsync(Guid TeacherId)
        {
            return await _teachers.Where(t => t.Id == TeacherId).Include(c => c.Courses).FirstOrDefaultAsync();
        }

        public async Task<bool> RemoveCourseTeacherAsync(Guid TeacherId, Guid CourseId)
        {
            var existing = await _teachers.Where(s => s.Id == TeacherId).Include(c => c.Courses).FirstOrDefaultAsync();

            if(existing == null)
            {
                throw new ApiExceptions("Teacher not found");
            }
            else
            {
                if(!existing.Courses.Any(c => c.Id == CourseId))
                {
                    throw new ApiExceptions("Teacher is not teaching this course");
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

        public async Task<Teacher> UpdateTeacherAsync(Teacher teacher)
        {
            var existing = await _teachers.Where(s => s.Id == teacher.Id).FirstOrDefaultAsync();

            if(existing == null)
            {
                throw new ApiExceptions("Teacher not found");
            }
            else
            {
                _context.Entry(existing).CurrentValues.SetValues(new {
                    FirstName = teacher.FirstName,
                    LastName = teacher.LastName,
                    SubjectClassifier = teacher.SubjectClassifier
                });
                var result = await _context.SaveChangesAsync();
                return await _teachers.Where(t => t.Id == teacher.Id).FirstOrDefaultAsync();


                // if(result == 0)
                // {
                // }
                // else
                // {
                //    throw new ApiExceptions("Error updating teacher");
                // }
            }
        }
    }
}