

using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using OCASAPI.Application.DTO.Requests;
using OCASAPI.Application.Interfaces;
using OCASAPI.Application.Wrappers;
using OCASAPI.Infrastructure.Context;
using OCASAPI.Infrastructure.LoggingAdapter;
using OCASAPI.Infrastructure.Models;
using OCASAPI.Infrastructure.Repositories;
using OCASAPI.Infrastructure.Services;

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

            services.AddDbContext<IdentityContext>(options => {
                options.EnableSensitiveDataLogging();
                options.UseSqlServer(configuration.GetConnectionString("AuthConnection"), b => b.MigrationsAssembly(typeof(IdentityContext).Assembly.FullName));
            });
            
            //#region Services
            services.AddTransient<ICourseRepository, CourseRepository>();
            services.AddTransient<IStudentRepository, StudentRepository>();
            services.AddTransient<ITeacherRepository, TeacherRepository>();
            services.AddTransient<IGradeRepository, GradeRepository>();
            services.AddTransient<ISchoolRepository, SchoolRepository>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddTransient(typeof(IAppLogger<>), typeof(LoggerAdapter<>));


            //response caching service
            services.AddResponseCaching(options =>
            {
                options.UseCaseSensitivePaths = true;
                options.MaximumBodySize = 1024;
            });

            services.AddIdentity<User, Role>().AddEntityFrameworkStores<IdentityContext>().AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options => 
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 8; 
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromHours(1);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = false;

                // User settings.
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+!$*^";
                options.User.RequireUniqueEmail = true;
            });

            services.AddTransient<IValidator<RegistrationRequest>, RegistrationRequestValidator>();



            //add authentication defaults for validating Identity Server4 tokens
            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
                {
                    o.SaveToken = true;
                    o.Audience = "";
                    o.Authority = "";
                    o.RequireHttpsMetadata = false; 
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = true,
                        ValidateIssuer = true,
                        RoleClaimType = "role",
                        ValidIssuer = "",
                        ValidAudience = ""
                    };
                    o.TokenValidationParameters.ValidTypes = new[] { "jwt" };

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