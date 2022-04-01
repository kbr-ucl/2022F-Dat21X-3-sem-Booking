using System.ComponentModel;
using AutoMapper;
using Booking.Contract;
using Booking.Contract.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Booking.Web.Pages.Booking;

public class DeleteModel : PageModel
{
    private readonly IBookingService _bookingService;
    private readonly IMapper _mapper;

    public DeleteModel(IBookingService bookingService, IMapper mapper)
    {
        _bookingService = bookingService;
        _mapper = mapper;
    }

    [FromRoute] public Guid Id { get; set; }
    [BindProperty] public BookingDeleteModel Booking { get; set; } = new();


    public async Task<IActionResult> OnGetAsync(Guid? id)
    {
        if (id == null) return NotFound();

        var domainBooking = await _bookingService.GetAsync(id.Value);
        if (domainBooking == null) return NotFound();

        Booking = _mapper.Map<BookingDeleteModel>(domainBooking);

        return Page();
    }

    //https://stackoverflow.com/questions/55602172/asp-net-core-razor-pages-support-for-delete-and-put-requests
    public async Task<IActionResult> OnPostAsync(Guid? id)
    {
        if (id == null) return NotFound();

        await _bookingService.DeleteAsync(id.Value);

        return RedirectToPage("./Index");
    }

    public class BookingDeleteModel
    {
        public Guid Id { get; set; }
        [DisplayName("Start tidspunkt")] public DateTime Start { get; set; }
        [DisplayName("Slut tidspunkt")] public DateTime Slut { get; set; }

        public BookingDto GetAsBookingDto()
        {
            return new BookingDto {Id = Id, Start = Start, Slut = Slut};
        }
    }
}