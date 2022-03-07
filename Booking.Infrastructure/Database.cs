using Booking.Application.Contract.Dtos;

namespace Booking.Infrastructure;

internal class Database
{
    public static Dictionary<Guid, BookingQueryDto> Bookings { get; } = new();
}