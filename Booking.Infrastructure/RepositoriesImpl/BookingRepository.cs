using Booking.Application.Infrastructure;
using Booking.Infrastructure.Database;

namespace Booking.Infrastructure.RepositoriesImpl;

public class BookingRepository : IBookingRepository
{
    private readonly BookingContext _db;

    public BookingRepository(BookingContext db)
    {
        _db = db;
    }

    void IBookingRepository.Add(Domain.Entities.Booking booking)
    {
        _db.Bookings.Add(booking);
        _db.SaveChanges();
    }

    void IBookingRepository.Delete(Guid id)
    {
        var booking = _db.Bookings.Find(id);
        if (booking is null) return;

        _db.Bookings.Remove(booking);
        _db.SaveChanges();
    }

    Domain.Entities.Booking IBookingRepository.Get(Guid id)
    {
        return _db.Bookings.Find(id) ?? throw new Exception("Booking not found");
    }

    void IBookingRepository.Save(Domain.Entities.Booking booking)
    {
        _db.Bookings.Update(booking);
        _db.SaveChanges();
    }
}