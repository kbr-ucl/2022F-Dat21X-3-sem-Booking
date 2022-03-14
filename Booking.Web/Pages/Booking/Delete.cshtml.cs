using System.ComponentModel;
using Booking.Application.Contract;
using Booking.Application.Contract.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Booking.Web.Pages.Booking
{
    public class DeleteModel : PageModel
    {
        private readonly IBookingQuery _bookingQuery;
        private readonly IBookingCommand _bookingCommand;

        public DeleteModel(IBookingQuery bookingQuery, IBookingCommand bookingCommand)
        {
            _bookingQuery = bookingQuery;
            _bookingCommand = bookingCommand;
        }

        [FromRoute] public Guid Id { get; set; }
        [BindProperty] public BookingDeleteModel Booking { get; set; } = new();


        public IActionResult OnGet(Guid? id)
        {
            if (id == null) return NotFound();

            var domainBooking = _bookingQuery.GetBooking(id.Value);
            if (domainBooking == null) return NotFound();

            Booking = BookingDeleteModel.CreateFromBookingQueryDto(domainBooking);

            return Page();
        }

        //https://stackoverflow.com/questions/55602172/asp-net-core-razor-pages-support-for-delete-and-put-requests
        public IActionResult OnPostAsync(Guid? id)
        {
            if (id == null) return NotFound();

            _bookingCommand.Delete(id.Value);

            return RedirectToPage("./Index");
        }

        public class BookingDeleteModel
        {
            public BookingDeleteModel()
            {
            }

            private BookingDeleteModel(BookingQueryDto booking)
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

            public static BookingDeleteModel CreateFromBookingQueryDto(BookingQueryDto booking)
            {
                return new BookingDeleteModel(booking);
            }
        }
    }
}
