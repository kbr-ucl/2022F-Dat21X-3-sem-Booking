using Booking.Contract.Dtos;

namespace Booking.Contract;

public interface IBookingService
{
    Task CreateAsync(BookingDto bookingDto);
    Task EditAsync(BookingDto bookingDto);
    Task DeleteAsync(Guid id);
    Task<BookingDto?> GetAsync(Guid id);
    Task<IEnumerable<BookingDto>> GetAsync();
}