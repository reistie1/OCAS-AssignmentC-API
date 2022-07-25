using Microsoft.EntityFrameworkCore;
using OCAS.Domain.Common;

namespace OCASAPI.Infrastructure.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options){}

        public virtual DbSet<Address> Address {get; set;}
        public virtual DbSet<Course> Courses {get; set;}
        public virtual DbSet<Grade> Grades {get; set;}
        public virtual DbSet<School> Schools {get; set;} 
        public virtual DbSet<Student> Students {get; set;}
        public virtual DbSet<Teacher> Teachers {get; set;}

        /// <summary>
        /// configure database schema for identity framework
        /// </summary>
        /// <param name="builder"></param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}