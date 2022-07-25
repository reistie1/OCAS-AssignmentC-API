using Microsoft.EntityFrameworkCore;
using OCASAPI.Infrastructure.Context;
using OCASAPI.Infrastructure.Extensions;
using OCASAPI.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Configuration.AddEnvironmentVariables();
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

ServiceExtensions.AddSwaggerExtension(builder.Services);
ServiceExtension.AddIdentityInfrastructure(builder.Services, builder.Configuration);



var app = builder.Build();

var service = (IServiceScopeFactory)app.Services.GetService(typeof(IServiceScopeFactory));

using (var scope = app.Services.CreateScope())
using(var db = service.CreateScope().ServiceProvider.GetService<ApplicationContext>()) 
using(var authdb = service.CreateScope().ServiceProvider.GetService<IdentityContext>()) 
{
    try
    {
        if(!db.Database.EnsureCreated())
        {
            db.Database.Migrate();
            authdb.Database.Migrate();
        }
        else
        {
            db.Database.Migrate();
            authdb.Database.Migrate();
        }
        Console.WriteLine("Applying Migrations");
    }
    catch (Exception e)
    {
        Console.WriteLine("An error occurred creating the DB");
    }
}


AppExtensions.UseSwaggerExtension(app);
AppExtensions.UseErrorHandlingMiddleware(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
