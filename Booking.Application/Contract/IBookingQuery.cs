using Booking.Application.Contract.Dtos;

namespace Booking.Application.Contract;

public interface IBookingQuery
{
    Task<BookingQueryDto?> GetBookingAsync(Guid id);
    Task<IEnumerable<BookingQueryDto>> GetBookingsAsync();
}