using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Booking.Infrastructure.Database;

public class BookingContext : DbContext
{
    public BookingContext(DbContextOptions<BookingContext> options)
        : base(options)
    {
    }

    public DbSet<Domain.Entities.Booking> Bookings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //this will apply configs from separate classes
        //which implemented IEntityTypeConfiguration<T>
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}