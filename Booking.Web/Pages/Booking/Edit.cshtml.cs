using System.ComponentModel;
using Booking.Contract;
using Booking.Contract.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Booking.Web.Pages.Booking;

public class EditModel : PageModel
{
    private readonly IBookingService _bookingService;

    public EditModel(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    [FromRoute] public Guid Id { get; set; }

    [BindProperty] public BookingEditModel Booking { get; set; } = new();


    public IActionResult OnGet(Guid? id)
    {
        if (id == null) return NotFound();

        var domainBooking = _bookingService.Get(id.Value);
        if (domainBooking == null) return NotFound();

        Booking = BookingEditModel.CreateFromBookingDto(domainBooking);

        return Page();
    }

    public IActionResult OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();

        _bookingService.Edit(Booking.GetAsBookingDto());

        return RedirectToPage("./Index");
    }

    public class BookingEditModel
    {
        public BookingEditModel()
        {
        }

        private BookingEditModel(BookingDto booking)
        {
            Id = booking.Id;
            Start = booking.Start;
            Slut = booking.Slut;
        }

        public Guid Id { get; set; }
        [DisplayName("Start tidspunkt")] public DateTime Start { get; set; }
        [DisplayName("Slut tidspunkt")] public DateTime Slut { get; set; }

        public BookingDto GetAsBookingDto()
        {
            return new BookingDto {Id = Id, Start = Start, Slut = Slut};
        }

        public static BookingEditModel CreateFromBookingDto(BookingDto booking)
        {
            return new BookingEditModel(booking);
        }
    }
}