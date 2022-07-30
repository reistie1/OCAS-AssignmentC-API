using Microsoft.EntityFrameworkCore;
using OCASAPI.Application.Extensions;
using OCASAPI.Infrastructure.Context;
using OCASAPI.Infrastructure.Extensions;
using OCASAPI.Infrastructure.Seeds;
using OCASAPI.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Configuration.AddEnvironmentVariables();
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
// builder.Services.AddControllersWithViews().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
builder.Services.AddCors(o => o.AddPolicy("Cors", builder => {
    builder.AllowAnyMethod().AllowAnyHeader().WithOrigins("http://localhost:4200").AllowCredentials();
}));

ServiceExtensions.AddSwaggerExtension(builder.Services);
ServiceExtension.AddIdentityInfrastructure(builder.Services, builder.Configuration);
ApplicationServiceExtensions.AddApplicationLayer(builder.Services);


var app = builder.Build();

var service = (IServiceScopeFactory)app.Services.GetService(typeof(IServiceScopeFactory));

using (var scope = app.Services.CreateScope())
using(var db = service.CreateScope().ServiceProvider.GetService<ApplicationContext>()) 
{
    try
    {
        if(!db.Database.EnsureCreated())
        { 
            db.Database.Migrate();
            seedActivityList(db);
        }
        else
        {
            db.Database.Migrate();
            seedActivityList(db);
        }
        Console.WriteLine("Applying Migrations");
    }
    catch (Exception)
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
app.UseRouting();
app.UseErrorHandlingMiddleware();
app.UseCors("Cors");
app.UseAuthorization();

// app.MapControllers();
app.UseEndpoints(endpoints => {
    endpoints.MapControllers();
});

app.Run();

void seedActivityList(ApplicationContext context)
{
    if(!context.Activities.Any())
    {
        var seed = new SeedActivities(context);
        context.Activities.AddRange(seed.createList());
        context.SaveChanges();
    }
}
