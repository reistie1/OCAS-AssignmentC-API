using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;


/// <summary>
/// Add swagger docs and api versioning services to the application container, 
/// </summary>
namespace OCASAPI.WebApi.Extensions
{
    /// <summary>
    /// Static class to add services to the services collection for the application
    /// </summary>
    public static class ServiceExtensions
    {
        /// <summary>
        /// Add swagger service to services collection used by the application
        /// </summary>
        /// <param name="services">Instance of <see cref="Microsoft.Extensions.DependencyInjection.IServiceCollection"/></param>
        public static void AddSwaggerExtension(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo 
                {
                    Version = "v1",
                    Title = "OCAS - AssessmentAPI",
                    Description = "AssessmentAPI for OCAS",
                    Contact = new OpenApiContact
                    {
                        Name = "Josh Reist",
                        Email = "josh.reist@gmail.com",
                    }
                });
            });
        }

        /// <summary>
        /// Add API Versioning service to the services collection for the application
        /// </summary>
        /// <param name="services">Instance of <see cref="Microsoft.Extensions.DependencyInjection.IServiceCollection"/></param>
        public static void AddApiVersioningExtension(this IServiceCollection services)
        {
            services.AddApiVersioning(config =>
            {
                // Specify the default API Version
                config.DefaultApiVersion = new ApiVersion(1, 0);
                // If the client hasn't specified the API version in the request, use the default API version number 
                config.AssumeDefaultVersionWhenUnspecified = true;
                // Advertise the API versions supported for the particular endpoint
                config.ReportApiVersions = true;
             });
        }
    }
}