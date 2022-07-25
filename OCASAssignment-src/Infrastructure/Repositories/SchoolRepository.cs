using Microsoft.EntityFrameworkCore;
using OCAS.Domain.Common;
using OCASAPI.Application.Exceptions;
using OCASAPI.Infrastructure.Context;

namespace OCASAPI.Infrastructure.Repositories
{
    public class SchoolRepository : ISchoolRepository
    {
        private readonly ApplicationContext _context;
        private readonly DbSet<School> _school;
        public SchoolRepository(ApplicationContext context)
        {
            _context = context;
            _school = context.Set<School>();
        }

        public async Task<School> GetSchoolInformationAsync(Guid SchoolId)
        {
            var existing = await _school.Where(s => s.Id == SchoolId).FirstOrDefaultAsync();

            if(existing == null)
            {
                throw new ApiExceptions("School not found");
            }
            else
            {
                return existing;
            }
        }

        public async Task<School> UpdateSchoolInformationAsync(School school)
        {
            var existing = await _school.Where(s => s.Id == school.Id).FirstOrDefaultAsync();

            if(existing == null)
            {
                throw new ApiExceptions("School not found");
            }
            else
            {
                _context.Entry(existing).CurrentValues.SetValues(new {Name = school.Name});
                _context.Entry(existing.Address).CurrentValues.SetValues(school.Address);

                var result = await _context.SaveChangesAsync();

                if(result == 0)
                {
                    return await this.GetSchoolInformationAsync(school.Id);
                }
                else
                {
                    throw new ApiExceptions("Error saving school");
                }
            }
        }
    }
}