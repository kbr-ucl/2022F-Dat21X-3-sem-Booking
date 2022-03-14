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
        modelBuilder.Entity<Domain.Entities.Booking>(model =>
        {
            model.HasKey(b => b.Id);
            model.Ignore(b => b.ServiceProvider);
        });

    }
}