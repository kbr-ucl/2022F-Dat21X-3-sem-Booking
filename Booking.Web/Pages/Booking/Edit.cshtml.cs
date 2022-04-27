using System.ComponentModel;
using Booking.Contract;
using Booking.Contract.Dtos;
using Booking.Web.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Booking.Web.Pages.Booking;

public class EditModel : PageModel
{
    private readonly IBookingService _bookingService;
    private readonly IAuthorizationService _authService;

    public EditModel(IBookingService bookingService, IAuthorizationService authService)
    {
        _bookingService = bookingService;
        _authService = authService;
    }

    [FromRoute] public Guid Id { get; set; }

    [BindProperty] public BookingEditModel Booking { get; set; } = new();


    public async Task<IActionResult> OnGetAsync(Guid? id)
    {
        if (id == null) return NotFound();

        var domainBooking = await _bookingService.GetAsync(id.Value);
        if (domainBooking == null) return NotFound();

        Booking = BookingEditModel.CreateFromBookingDto(domainBooking);

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var authResult = await _authService.AuthorizeAsync(User, PolicyEnum.AdminOnly);
        if (!authResult.Succeeded)
        {
            return new ForbidResult();
        }

        if (!ModelState.IsValid) return Page();
        try
        {
            await _bookingService.EditAsync(Booking.GetAsBookingDto());
        }
        catch (Exception e)
        {
            ModelState.AddModelError(String.Empty, e.Message);
            return Page();
        }

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
            Version = booking.Version;
        }

        public Guid Id { get; set; }
        [DisplayName("Start tidspunkt")] public DateTime Start { get; set; }
        [DisplayName("Slut tidspunkt")] public DateTime Slut { get; set; }
        public byte[] Version { get; set; }

        public BookingDto GetAsBookingDto()
        {
            return new BookingDto {Id = Id, Start = Start, Slut = Slut, Version = Version};
        }

        public static BookingEditModel CreateFromBookingDto(BookingDto booking)
        {
            return new BookingEditModel(booking);
        }
    }
}