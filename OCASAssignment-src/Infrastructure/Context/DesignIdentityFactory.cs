using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace OCASAPI.Infrastructure.Context
{
    public class DesignIdentityFactory : IDesignTimeDbContextFactory<IdentityContext>
    {
        private static string DbConnectionString => new DatabaseConfiguration().GetDataConnectionString();

        public IdentityContext CreateDbContext(string[] args)
        {

            var optionsBuilder = new DbContextOptionsBuilder<IdentityContext>();
            optionsBuilder.UseSqlServer(DbConnectionString);

            return new IdentityContext(optionsBuilder.Options);
        }

    /// <summary>
    /// Abstract class to add the appsettings.json file to our app configuration, containing our connection string
    /// </summary>
    public abstract class ConfigurationBase
    {
        protected IConfigurationRoot GetConfiguration()
        {
            return new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build(); 
        }
    }

    /// <summary>
    /// Set connection string from appsettings.json file added to configuration
    /// </summary>
    public class DatabaseConfiguration : ConfigurationBase
    {
        private string DataConnectionKey = "AuthConnection";

        public string GetDataConnectionString()
        {
            return GetConfiguration().GetConnectionString(DataConnectionKey);
        }
    }
    }
}