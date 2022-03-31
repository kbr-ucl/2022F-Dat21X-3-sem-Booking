using Booking.Application.Contract;
using Booking.Application.Contract.Dtos;
using Booking.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Booking.Infrastructure.Queries;

public class BookingQuery : IBookingQuery
{
    private readonly BookingContext _db;

    public BookingQuery(BookingContext db)
    {
        _db = db;
    }

    async Task<BookingQueryDto?> IBookingQuery.GetBookingAsync(Guid id)
    {
        var result = await _db.Bookings.FindAsync(id);
        if (result is null) return new BookingQueryDto();

        return new BookingQueryDto
        {
            Id = result.Id,
            Start = result.Start,
            Slut = result.Slut
        };
    }

    async Task<IEnumerable<BookingQueryDto>> IBookingQuery.GetBookingsAsync()
    {
        var result = new List<BookingQueryDto>();
        var dbBookings = await _db.Bookings.ToListAsync();
        dbBookings.ForEach(a => result.Add(new BookingQueryDto
        {
            Id = a.Id,
            Start = a.Start,
            Slut = a.Slut
        }));
        return result;
    }
}