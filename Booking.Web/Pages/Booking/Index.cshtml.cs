using System.ComponentModel;
using Booking.Contract;
using Booking.Contract.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Booking.Web.Pages.Booking;

public class IndexModel : PageModel
{
    private readonly IBookingService _bookingService;

    public IndexModel(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    [BindProperty] public IEnumerable<BookingIndexModel> Bookings { get; set; } = Enumerable.Empty<BookingIndexModel>();

    public async Task OnGetAsync()
    {
        var bookings = new List<BookingIndexModel>();
        var dbBookings = await _bookingService.GetAsync();
        dbBookings.ToList().ForEach(a => bookings.Add(new BookingIndexModel(a)));
        Bookings = bookings;
    }

    public class BookingIndexModel
    {
        public BookingIndexModel(BookingDto booking)
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