using Booking.Application.Contract;

namespace Booking.Application.Implementation;

public class BookingCommands : IBookingCommands
{
    private readonly IServiceProvider _serviceProvider;

    public BookingCommands(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
}