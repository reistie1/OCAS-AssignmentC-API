using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OCAS.Domain.Common;
using OCASAPI.Infrastructure.Models;

namespace OCASAPI.Infrastructure.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options){}

        public DbSet<Address> Address;
        public DbSet<Course> Courses;
        public DbSet<Grade> Grades;
        public DbSet<School> Schools; 
        public DbSet<Student> Students;
        public DbSet<Teacher> Teachers;
        public DbSet<Tokens> Tokens;
        public DbSet<Roles> Roles;
        public DbSet<User> Users;

        /// <summary>
        /// configure database schema for Identity framework entity tables
        /// </summary>
        /// <param name="builder"></param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("UserClaims");
            });

            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable("UserLogins"); 
                entity.HasKey(o => o.UserId);
            });

            builder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.ToTable("RoleClaims");
            });

            builder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("UserTokens");
                entity.HasKey(o => o.UserId);
            });
        }
    }
}