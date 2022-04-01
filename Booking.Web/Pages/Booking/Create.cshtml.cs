using System.ComponentModel;
using AutoMapper;
using Booking.Contract;
using Booking.Contract.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Booking.Web.Pages.Booking;

public class CreateModel : PageModel
{
    private readonly IBookingService _bookingService;
    private readonly IMapper _mapper;

    public CreateModel(IBookingService bookingService, IMapper mapper)
    {
        _bookingService = bookingService;
        _mapper = mapper;
    }

    [BindProperty] public BookingCreateModel Booking { get; set; } = new();

    public void OnGet()
    {
        Booking = new BookingCreateModel();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();

        await _bookingService.CreateAsync(_mapper.Map<BookingDto>(Booking));
        return RedirectToPage("./Index");
    }

    public class BookingCreateModel
    {
        public Guid Id { get; set; }

        [DisplayName("Start tidspunkt")] public DateTime Start { get; set; } = DateTime.Now;

        [DisplayName("Slut tidspunkt")] public DateTime Slut { get; set; } = DateTime.Now + TimeSpan.FromMinutes(30);
    }
}