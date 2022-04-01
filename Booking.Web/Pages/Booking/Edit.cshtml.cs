using System.ComponentModel;
using AutoMapper;
using Booking.Contract;
using Booking.Contract.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Booking.Web.Pages.Booking;

public class EditModel : PageModel
{
    private readonly IBookingService _bookingService;
    private readonly IMapper _mapper;

    public EditModel(IBookingService bookingService, IMapper mapper)
    {
        _bookingService = bookingService;
        _mapper = mapper;
    }

    [FromRoute] public Guid Id { get; set; }

    [BindProperty] public BookingEditModel Booking { get; set; } = new();


    public async Task<IActionResult> OnGetAsync(Guid? id)
    {
        if (id == null) return NotFound();

        var domainBooking = await _bookingService.GetAsync(id.Value);
        if (domainBooking == null) return NotFound();

        Booking = _mapper.Map<BookingEditModel>(domainBooking);

        return Page();
    }

    public IActionResult OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();

        _bookingService.EditAsync(_mapper.Map<BookingDto>(Booking));

        return RedirectToPage("./Index");
    }

    public class BookingEditModel
    {
        public Guid Id { get; set; }
        [DisplayName("Start tidspunkt")] public DateTime Start { get; set; }
        [DisplayName("Slut tidspunkt")] public DateTime Slut { get; set; }
    }
}