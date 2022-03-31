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

    async Task IBookingCommand.CreateAsync(BookingCommandDto bookingDto)
    {
        var booking = new Booking.Domain.Entities.Booking(_serviceProvider, bookingDto.Start, bookingDto.Slut);
        await _repository.AddAsync(booking);
    }

    async Task IBookingCommand.DeleteAsync(BookingCommandDto bookingDto)
    {
        await _repository.DeleteAsync(bookingDto.Id);
    }

    async Task IBookingCommand.EditAsync(BookingCommandDto bookingDto)
    {
        var booking = await _repository.GetAsync(bookingDto.Id);
        booking.ServiceProvider = _serviceProvider;
        booking.Update(bookingDto.Start, bookingDto.Slut, bookingDto.Version);
        await _repository.SaveAsync(booking);
    }
}