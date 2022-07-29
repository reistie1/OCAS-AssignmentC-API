using Microsoft.EntityFrameworkCore;
using OCASAPI.Application.Interfaces;
using OCASAPI.Infrastructure.Context;
using OCASAPI.Infrastructure.LoggingAdapter;
using OCASAPI.Infrastructure.Repositories;

namespace OCASAPI.Infrastructure.Extensions
{
    /// <summary>
    /// Service extensions class to add all the services used by the infrastructure portion of the api application
    /// </summary>
    public static class ServiceExtension
    {
        /// <summary>
        /// Add Identity Infrastructure configures the default connection to the database, mail api key, 
        /// authentication and authorization defaults for the api, and transients for the services utilized in the api infrastructure layer.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddIdentityInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationContext>(options => {
                options.EnableSensitiveDataLogging();
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName));
            });
            
            //#region Services
            services.AddTransient(typeof(IAppLogger<>), typeof(LoggerAdapter<>));
            services.AddTransient<IActivityRepository, ActivityRepository>();


            //response caching service
            services.AddResponseCaching(options =>
            {
                options.UseCaseSensitivePaths = true;
                options.MaximumBodySize = 1024;
            });
        }
    }
}