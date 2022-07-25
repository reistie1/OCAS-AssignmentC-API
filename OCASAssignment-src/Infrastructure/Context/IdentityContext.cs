using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OCASAPI.Infrastructure.Models;

namespace OCASAPI.Infrastructure.Context
{
    public class IdentityContext : IdentityDbContext<User,Role,Guid>
    {
        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options){}

        public DbSet<Tokens> Tokens;
        public DbSet<Role> Role;
        public DbSet<User> User;

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