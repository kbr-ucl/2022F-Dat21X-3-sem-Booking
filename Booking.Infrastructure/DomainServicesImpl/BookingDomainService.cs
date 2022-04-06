using Booking.Domain.DomainServices;
using Booking.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Booking.Infrastructure.DomainServicesImpl;

public class BookingDomainService : IBookingDomainService
{
    private readonly BookingContext _db;

    public BookingDomainService(BookingContext db)
    {
        _db = db;
    }
    IEnumerable<Domain.Entities.Booking> IBookingDomainService.GetOtherBookings(Domain.Entities.Booking booking)
    {
        return _db.Bookings.Where(b => b.Id != booking.Id).ToList();
    }
}