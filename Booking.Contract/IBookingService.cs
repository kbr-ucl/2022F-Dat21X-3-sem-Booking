using Booking.Contract.Dtos;

namespace Booking.Contract;

public interface IBookingService
{
    void Create(BookingDto bookingDto);
    void Edit(BookingDto bookingDto);
    void Delete(BookingDto bookingDto);
    BookingDto? Get(Guid id);
    IEnumerable<BookingDto> Get();
}