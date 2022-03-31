using Booking.Application.Contract.Dtos;

namespace Booking.Application.Contract;

public interface IBookingCommand
{
    Task CreateAsync(BookingCommandDto bookingDto);
    Task EditAsync(BookingCommandDto bookingDto);
    Task DeleteAsync(BookingCommandDto bookingDto);
}