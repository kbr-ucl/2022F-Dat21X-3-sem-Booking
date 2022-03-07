using Booking.Application.Contract;
using Booking.Application.Contract.Dtos;
using Booking.Application.Infrastructure;

namespace Booking.Application.Implementation;

public class BookingCommand : IBookingCommand
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IBookingRepository _repository;

    public BookingCommand(IServiceProvider serviceProvider, IBookingRepository repository)
    {
        _serviceProvider = serviceProvider;
        _repository = repository;
    }

    void IBookingCommand.Create(BookingCommandDto bookingDto)
    {
        var booking = new Booking.Domain.Entities.Booking(_serviceProvider, bookingDto.Start, bookingDto.Slut);
        _repository.Add(booking);
    }

    void IBookingCommand.Edit(BookingCommandDto bookingDto)
    {
        var booking = _repository.Get(bookingDto.Id);
        booking.ServiceProvider = _serviceProvider;
        booking.Update(bookingDto.Start, bookingDto.Slut);
        _repository.Save(booking);
    }
}