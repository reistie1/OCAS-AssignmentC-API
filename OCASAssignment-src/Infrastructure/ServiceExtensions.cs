

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using OCASAPI.Application.Wrappers;
using OCASAPI.Infrastructure.Context;

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
            string defaultConnection = "";

            services.AddDbContext<ApplicationContext>(options => {
                options.EnableSensitiveDataLogging();
                options.UseSqlServer(defaultConnection, b => b.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName));
            });
            
            //#region Services


            //response caching service
            services.AddResponseCaching(options =>
            {
                options.UseCaseSensitivePaths = true;
                options.MaximumBodySize = 1024;
            });


            //add authentication defaults for validating Identity Server4 tokens
            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
                {
                    o.SaveToken = true;
                    // o.Authority = authority;
                    // o.Audience = authority + "/resources";
                    o.RequireHttpsMetadata = false; 
                    //o.TokenValidationParameters = new TokenValidationParameters { RoleClaimType = "role",  ValidateAudience = true, ValidateIssuer = true, ValidIssuer = authority, ValidAudience = authority + "/resources" };
                    o.TokenValidationParameters.ValidTypes = new[] { "at+jwt" };

                    o.Events = new JwtBearerEvents()
                    {
                        OnChallenge = context =>
                        {
                            context.HandleResponse();
                            context.Response.StatusCode = 401;
                            context.Response.ContentType = "application/json";
                            var result = JsonConvert.SerializeObject(new Response<string>("You are not Authorized"));
                            return context.Response.WriteAsync(result);
                        },
                        OnForbidden = context =>
                        {
                            context.Response.StatusCode = 403;
                            context.Response.ContentType = "application/json";
                            var result = JsonConvert.SerializeObject(new Response<string>("You are not authorized to access this resource"));
                            return context.Response.WriteAsync(result);
                        },
                        OnMessageReceived = context =>
                        {
                            
                            string url = context.Request.Path.Value.ToLower();

                            if (context.Request.Cookies.ContainsKey("X-ACCESS-TOKEN") && !url.Equals("/api/v1/identity/register") && !url.Equals("/api/v1/identity/login"))
                            {                                
                                context.Token = context.Request.Cookies["X-ACCESS-TOKEN"];
                            }

                            return Task.CompletedTask;
                        }
                    };
                });

        }

       
    }
}