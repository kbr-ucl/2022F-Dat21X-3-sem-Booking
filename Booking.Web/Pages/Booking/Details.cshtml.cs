using System.ComponentModel;
using Booking.Contract;
using Booking.Contract.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Booking.Web.Pages.Booking;

public class DetailsModel : PageModel
{
    private readonly IBookingService _bookingService;

    public DetailsModel(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    [FromRoute] public Guid Id { get; set; }
    [BindProperty] public BookingDetailsModel Booking { get; set; } = new();


    public async Task<IActionResult> OnGetAsync(Guid? id)
    {
        if (id == null) return NotFound();

        var domainBooking = await _bookingService.GetAsync(id.Value);
        if (domainBooking == null) return NotFound();

        Booking = BookingDetailsModel.CreateFromBookingDto(domainBooking);

        return Page();
    }

    public class BookingDetailsModel
    {
        public BookingDetailsModel()
        {
        }

        private BookingDetailsModel(BookingDto booking)
        {
            Id = booking.Id;
            Start = booking.Start;
            Slut = booking.Slut;
        }

        public Guid Id { get; set; }
        [DisplayName("Start tidspunkt")] public DateTime Start { get; set; }
        [DisplayName("Slut tidspunkt")] public DateTime Slut { get; set; }

        public BookingDto GetAsBookingCommandDto()
        {
            return new BookingDto {Id = Id, Start = Start, Slut = Slut};
        }

        public static BookingDetailsModel CreateFromBookingDto(BookingDto booking)
        {
            return new BookingDetailsModel(booking);
        }
    }
}