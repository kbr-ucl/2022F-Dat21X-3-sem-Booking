using Booking.Application.Contract.Dtos;

namespace Booking.Application.Contract;

public interface IBookingQuery
{
    BookingQueryDto? GetBooking(Guid id);
    IEnumerable<BookingQueryDto> GetBookings();
}