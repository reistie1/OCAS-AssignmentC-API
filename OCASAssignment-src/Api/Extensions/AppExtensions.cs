
using OCASAPI.WebAPI.Middleware;
/// <summary>
/// App extension to register swagger extension and error handling middleware for the application 
/// </summary>
namespace OCASAPI.WebApi.Extensions
{
    /// <summary>
    /// Static class to abstract registering services relevant to the current API layer
    /// </summary>
    public static class AppExtensions
    {
        /// <summary>
        /// Register swagger extension using application builder
        /// </summary>
        /// <param name="app">Instance of <see cref="Microsoft.AspNetCore.Builder.ApplicationBuilder"/></param>
        public static void UseSwaggerExtension(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "OCASTEST.WebApi");
            });
        }

        /// <summary>
        /// Register error handling middleware using application builder 
        /// </summary>
        /// <param name="app">Instance of <see cref="Microsoft.AspNetCore.Builder.ApplicationBuilder"/></param>
        public static void UseErrorHandlingMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorHandlerMiddleware>();
        }
    }
}
