using System.ComponentModel;
using Booking.Contract;
using Booking.Contract.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Booking.Web.Pages.Booking;

public class CreateModel : PageModel
{
    private readonly IBookingService _bookingService;

    public CreateModel(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    [BindProperty] public BookingCreateModel Booking { get; set; } = new();

    public void OnGet()
    {
        Booking = new BookingCreateModel();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();

        await _bookingService.CreateAsync(Booking.GetAsBookingDto());
        return RedirectToPage("./Index");
    }

    public class BookingCreateModel
    {
        public Guid Id { get; set; }

        [DisplayName("Start tidspunkt")] public DateTime Start { get; set; } = DateTime.Now;

        [DisplayName("Slut tidspunkt")] public DateTime Slut { get; set; } = DateTime.Now + TimeSpan.FromMinutes(30);

        public BookingDto GetAsBookingDto()
        {
            return new BookingDto {Start = Start, Slut = Slut};
        }
    }
}