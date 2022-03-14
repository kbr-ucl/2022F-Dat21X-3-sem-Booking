using System.ComponentModel;
using Booking.Application.Contract;
using Booking.Application.Contract.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Booking.Web.Pages.Booking;

public class EditModel : PageModel
{
    private readonly IBookingCommand _bookingCommand;
    private readonly IBookingQuery _bookingQuery;

    public EditModel(IBookingQuery bookingQuery, IBookingCommand bookingCommand)
    {
        _bookingQuery = bookingQuery;
        _bookingCommand = bookingCommand;
    }

    [FromRoute] public Guid Id { get; set; }

    [BindProperty] public BookingEditModel Booking { get; set; } = new();


    public IActionResult OnGet(Guid? id)
    {
        if (id == null) return NotFound();

        var domainBooking = _bookingQuery.GetBooking(id.Value);
        if (domainBooking == null) return NotFound();

        Booking = BookingEditModel.CreateFromBookingQueryDto(domainBooking);

        return Page();
    }

    public IActionResult OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();

        _bookingCommand.Edit(Booking.GetAsBookingCommandDto());

        return RedirectToPage("./Index");
    }

    public class BookingEditModel
    {
        public BookingEditModel()
        {
        }

        private BookingEditModel(BookingQueryDto booking)
        {
            Id = booking.Id;
            Start = booking.Start;
            Slut = booking.Slut;
        }

        public Guid Id { get; set; }
        [DisplayName("Start tidspunkt")] public DateTime Start { get; set; }
        [DisplayName("Slut tidspunkt")] public DateTime Slut { get; set; }

        public BookingCommandDto GetAsBookingCommandDto()
        {
            return new BookingCommandDto {Id = Id, Start = Start, Slut = Slut};
        }

        public static BookingEditModel CreateFromBookingQueryDto(BookingQueryDto booking)
        {
            return new BookingEditModel(booking);
        }
    }
}