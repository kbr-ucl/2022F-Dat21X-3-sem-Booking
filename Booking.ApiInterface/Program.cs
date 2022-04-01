using System.Reflection;
using Booking.Application.Implementation;
using Booking.Infrastructure.Database;
using Booking.Infrastructure.DomainServicesImpl;
using Booking.Infrastructure.RepositoriesImpl;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/projects?tabs=dotnet-core-cli
// Add-Migration NewMigration -Project Booking.Infrastructure
// Update-Database

builder.Services.AddDbContext<BookingContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BookingContext"),
        x => { x.MigrationsAssembly("Booking.Infrastructure"); }));


builder.Services.Scan(scan =>
{
    scan.FromCallingAssembly()
        .AddClasses()
        .AsMatchingInterface();
    scan.FromAssembliesOf(typeof(BookingCommand)).AddClasses()
        .AsMatchingInterface();
    scan.FromAssembliesOf(typeof(BookingDomainService)).AddClasses()
        .AsMatchingInterface();
    scan.FromAssembliesOf(typeof(BookingRepository)).AddClasses()
        .AsMatchingInterface();
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "ToDo API",
        Description = "An ASP.NET Core Web API for managing ToDo items",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Example Contact",
            Url = new Uri("https://example.com/contact")
        },
        License = new OpenApiLicense
        {
            Name = "Example License",
            Url = new Uri("https://example.com/license")
        }
    });

    // using System.Reflection;
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

app.MapControllers();

app.Run();