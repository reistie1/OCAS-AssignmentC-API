using System.Reflection;
using FluentValidation;
using MediatR;
using OCASAPI.Application.Behaviours;

namespace OCASAPI.Application.Extensions
{
    public static class ApplicationServiceExtensions
    {
        /// <summary>
        /// Add services to the services collection used in the application layer of the application
        /// </summary>
        /// <param name="services">Instance of<see cref="Microsoft.Extensions.DependencyInjection.IServiceCollection"/></param>
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        }
    }
}