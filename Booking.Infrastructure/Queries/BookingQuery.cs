using Booking.Application.Contract;
using Booking.Application.Contract.Dtos;

namespace Booking.Infrastructure.Queries;

public class BookingQuery : IBookingQuery
{
    BookingQueryDto IBookingQuery.GetBooking(Guid id)
    {
        return Database.Bookings[id];
    }

    IEnumerable<BookingQueryDto> IBookingQuery.GetBookings()
    {
        return Database.Bookings.Values;
    }
}