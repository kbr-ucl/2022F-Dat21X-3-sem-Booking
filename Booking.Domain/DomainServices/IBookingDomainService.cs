namespace Booking.Domain.DomainServices;

public interface IBookingDomainService
{
    IEnumerable<Entities.Booking> GetExsistingBookings();
}