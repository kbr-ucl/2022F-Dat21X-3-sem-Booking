using System.ComponentModel;
using AutoMapper;
using Booking.Contract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Booking.Web.Pages.Booking;

public class DetailsModel : PageModel
{
    private readonly IBookingService _bookingService;
    private readonly IMapper _mapper;

    public DetailsModel(IBookingService bookingService, IMapper mapper)
    {
        _bookingService = bookingService;
        _mapper = mapper;
    }

    [FromRoute] public Guid Id { get; set; }
    [BindProperty] public BookingDetailsModel Booking { get; set; } = new();


    public async Task<IActionResult> OnGetAsync(Guid? id)
    {
        if (id == null) return NotFound();

        var domainBooking = await _bookingService.GetAsync(id.Value);
        if (domainBooking == null) return NotFound();

        Booking = Booking = _mapper.Map<BookingDetailsModel>(domainBooking);

        return Page();
    }

    public class BookingDetailsModel
    {
        public Guid Id { get; set; }
        [DisplayName("Start tidspunkt")] public DateTime Start { get; set; }
        [DisplayName("Slut tidspunkt")] public DateTime Slut { get; set; }
    }
}