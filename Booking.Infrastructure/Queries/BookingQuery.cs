using Booking.Application.Contract;
using Booking.Application.Contract.Dtos;
using Booking.Infrastructure.Database;

namespace Booking.Infrastructure.Queries;

public class BookingQuery : IBookingQuery
{
    private readonly BookingContext _db;

    public BookingQuery(BookingContext db)
    {
        _db = db;
    }

    BookingQueryDto IBookingQuery.GetBooking(Guid id)
    {
        var result = _db.Bookings.Find(id);
        if (result is null) return new BookingQueryDto();

        return new BookingQueryDto
        {
            Id = result.Id,
            Start = result.Start,
            Slut = result.Slut
        };
    }

    IEnumerable<BookingQueryDto> IBookingQuery.GetBookings()
    {
        var result = new List<BookingQueryDto>();
        _db.Bookings.ToList().ForEach(a => result.Add(new BookingQueryDto
        {
            Id = a.Id,
            Start = a.Start,
            Slut = a.Slut
        }));
        return result;
    }
}