using Booking.Domain.DomainServices;
using Booking.Infrastructure.Database;

namespace Booking.Infrastructure.DomainServicesImpl;

public class BookingDomainService : IBookingDomainService
{
    private readonly BookingContext _db;

    public BookingDomainService(BookingContext db)
    {
        _db = db;
    }
    IEnumerable<Domain.Entities.Booking> IBookingDomainService.GetExsistingBookings()
    {
        return _db.Bookings.ToList();
    }
}