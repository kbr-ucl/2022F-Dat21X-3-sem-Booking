using Booking.Application.Infrastructure;
using Booking.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Booking.Infrastructure.RepositoriesImpl;

public class BookingRepository : IBookingRepository
{
    private readonly BookingContext _db;

    public BookingRepository(BookingContext db)
    {
        _db = db;
    }

    async Task IBookingRepository.AddAsync(Domain.Entities.Booking booking)
    {
        _db.Bookings.Add(booking);
        await _db.SaveChangesAsync();
    }

    async Task IBookingRepository.DeleteAsync(Guid id)
    {
        var booking = _db.Bookings.Find(id);
        if (booking is null) return;

        _db.Bookings.Remove(booking);
        await _db.SaveChangesAsync();
    }

    async Task<Domain.Entities.Booking> IBookingRepository.GetAsync(Guid id)
    {
        return await _db.Bookings.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id) ?? throw new Exception("Booking not found");
    }

    async Task IBookingRepository.SaveAsync(Domain.Entities.Booking booking)
    {
        _db.Bookings.Update(booking);
        await _db.SaveChangesAsync();
    }
}