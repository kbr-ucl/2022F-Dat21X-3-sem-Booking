namespace Booking.Domain.DomainServices;

public interface IBookingDomainService
{
    IEnumerable<Entities.Booking> GetOtherBookings(Domain.Entities.Booking booking);
}