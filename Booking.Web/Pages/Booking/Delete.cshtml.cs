using System.ComponentModel;
using Booking.Contract;
using Booking.Contract.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Booking.Web.Pages.Booking;

public class DeleteModel : PageModel
{
    private readonly IBookingService _bookingService;

    public DeleteModel(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    [FromRoute] public Guid Id { get; set; }
    [BindProperty] public BookingDeleteModel Booking { get; set; } = new();


    public IActionResult OnGet(Guid? id)
    {
        if (id == null) return NotFound();

        var domainBooking = _bookingService.Get(id.Value);
        if (domainBooking == null) return NotFound();

        Booking = BookingDeleteModel.CreateFromBookingDto(domainBooking);

        return Page();
    }

    //https://stackoverflow.com/questions/55602172/asp-net-core-razor-pages-support-for-delete-and-put-requests
    public IActionResult OnPostAsync(Guid? id)
    {
        if (id == null) return NotFound();

        _bookingService.Delete(new BookingDto {Id = id.Value});

        return RedirectToPage("./Index");
    }

    public class BookingDeleteModel
    {
        public BookingDeleteModel()
        {
        }

        private BookingDeleteModel(BookingDto booking)
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

        public static BookingDeleteModel CreateFromBookingDto(BookingDto booking)
        {
            return new BookingDeleteModel(booking);
        }
    }
}