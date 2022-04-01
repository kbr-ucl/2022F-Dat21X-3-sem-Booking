using System.ComponentModel;
using AutoMapper;
using Booking.Contract;
using Booking.Contract.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Booking.Web.Pages.Booking;

public class IndexModel : PageModel
{
    private readonly IBookingService _bookingService;
    private readonly IMapper _mapper;

    public IndexModel(IBookingService bookingService, IMapper mapper)
    {
        _bookingService = bookingService;
        _mapper = mapper;
    }

    [BindProperty] public IEnumerable<BookingIndexModel> Bookings { get; set; } = Enumerable.Empty<BookingIndexModel>();

    public async Task OnGetAsync()
    {
        var dbBookings = await _bookingService.GetAsync();
        Bookings = _mapper.Map<IEnumerable<BookingDto>, IEnumerable<BookingIndexModel>>(dbBookings);
    }

    public class BookingIndexModel
    {
        public Guid Id { get; set; }

        [DisplayName("Start tidspunkt")] public DateTime Start { get; set; }

        [DisplayName("Slut tidspunkt")] public DateTime Slut { get; set; }
    }
}