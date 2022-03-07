using System.ComponentModel;
using Booking.Application.Contract;
using Booking.Application.Contract.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Booking.Web.Pages.Booking;

public class CreateModel : PageModel
{
    private readonly IBookingCommand _bookingCommand;

    public CreateModel(IBookingCommand bookingCommand)
    {
        _bookingCommand = bookingCommand;
    }

    [BindProperty] public BookingCreateModel Booking { get; set; } = new();

    public void OnGet()
    {
        Booking = new BookingCreateModel();
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid) return Page();

        _bookingCommand.Create(Booking.GetAsBookingCommandDto());
        return RedirectToPage("./Index");
    }

    public class BookingCreateModel
    {
        public Guid Id { get; set; }

        [DisplayName("Start tidspunkt")] public DateTime Start { get; set; } = DateTime.Now;

        [DisplayName("Slut tidspunkt")] public DateTime Slut { get; set; } = DateTime.Now + TimeSpan.FromMinutes(30);

        public BookingCommandDto GetAsBookingCommandDto()
        {
            return new BookingCommandDto {Start = Start, Slut = Slut};
        }
    }
}