using Microsoft.EntityFrameworkCore;
using OCAS.Domain.Common;

namespace OCASAPI.Infrastructure.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options){}

        public virtual DbSet<Activity> Activities {get; set;}
        public virtual DbSet<ActivitySignUp> ActivitySignup {get; set;}

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