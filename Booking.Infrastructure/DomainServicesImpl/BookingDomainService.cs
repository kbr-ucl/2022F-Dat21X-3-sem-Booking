using Booking.Domain.DomainServices;
namespace Booking.Infrastructure.DomainServicesImpl;

public class BookingDomainService : IBookingDomainService
{
    IEnumerable<Domain.Entities.Booking> IBookingDomainService.GetExsistingBookings()
    {
        var bookings = new List<Domain.Entities.Booking>();
        Database.Bookings.Values.ToList()
            .ForEach(a => bookings.Add(new Domain.Entities.Booking(a.Id, a.Start, a.Slut)));
        return bookings;
    }
}