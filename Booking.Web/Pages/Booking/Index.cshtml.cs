using System.ComponentModel;
using Booking.Application.Contract;
using Booking.Application.Contract.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Booking.Web.Pages.Booking;

public class IndexModel : PageModel
{
    private readonly IBookingQuery _bookingQuery;

    public IndexModel(IBookingQuery bookingQuery)
    {
        _bookingQuery = bookingQuery;
    }

    [BindProperty] public IEnumerable<BookingIndexModel> Bookings { get; set; } = Enumerable.Empty<BookingIndexModel>();

    public void OnGet()
    {
        var bookings = new List<BookingIndexModel>();
        _bookingQuery.GetBookings().ToList().ForEach(a => bookings.Add(new BookingIndexModel(a)));
        Bookings = bookings;
    }

    public class BookingIndexModel
    {
        public BookingIndexModel(BookingQueryDto booking)
        {
            Id = booking.Id;
            Start = booking.Start;
            Slut = booking.Slut;
        }

        public Guid Id { get; set; }

        [DisplayName("Start tidspunkt")] public DateTime Start { get; set; }

        [DisplayName("Slut tidspunkt")] public DateTime Slut { get; set; }
    }
}