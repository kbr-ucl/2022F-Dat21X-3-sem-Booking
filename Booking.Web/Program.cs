using Booking.Application.Contract;
using Booking.Application.Implementation;
using Booking.Application.Infrastructure;
using Booking.Domain.DomainServices;
using Booking.Infrastructure.Database;
using Booking.Infrastructure.DomainServicesImpl;
using Booking.Infrastructure.Queries;
using Booking.Infrastructure.RepositoriesImpl;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/projects?tabs=dotnet-core-cli
// Add-Migration NewMigration -Project Booking.Infrastructure
// Update-Database

builder.Services.AddDbContext<BookingContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BookingContext"), x =>
    {
        x.MigrationsAssembly("Booking.Infrastructure");
    }));

builder.Services.AddRazorPages();
builder.Services.AddScoped<IBookingQuery, BookingQuery>();
builder.Services.AddScoped<IBookingCommand, BookingCommand>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IBookingDomainService, BookingDomainService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
