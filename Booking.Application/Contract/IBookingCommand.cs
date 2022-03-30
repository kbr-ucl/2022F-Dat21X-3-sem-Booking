using Booking.Application.Contract.Dtos;

namespace Booking.Application.Contract;

public interface IBookingCommand
{
    void Create(BookingCommandDto bookingDto);
    void Edit(BookingCommandDto bookingDto);
    void Delete(BookingCommandDto bookingDto);
}