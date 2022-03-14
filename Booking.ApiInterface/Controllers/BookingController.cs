using Booking.Application.Contract;
using Booking.Application.Contract.Dtos;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Booking.ApiInterface.Controllers
{
    [Route("api/Booking")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingQuery _bookingQuery;
        private readonly IBookingCommand _bookingCommand;

        public BookingController(IBookingQuery bookingQuery, IBookingCommand bookingCommand)
        {
            _bookingQuery = bookingQuery;
            _bookingCommand = bookingCommand;
        }
        // GET: api/<BookingController>
        /// <summary>
        /// Her kan man skrive om GET
        /// </summary>
        [HttpGet]
        public IEnumerable<BookingQueryDto> Get()
        {
            return _bookingQuery.GetBookings().ToList();
        }

        // GET api/<BookingController>/5
        [HttpGet("{id}")]
        public ActionResult<BookingQueryDto> Get(Guid id)
        {
            var booking = _bookingQuery.GetBooking(id);
            if (booking is null) return NotFound();

            return booking;
        }

        // POST api/<BookingController>
        [HttpPost]
        public void Post([FromBody] BookingCommandDto value)
        {
            _bookingCommand.Create(value);
        }

        // PUT api/<BookingController>/5
        [HttpPut("{id}")]
        public void Put(Guid id, [FromBody] BookingCommandDto value)
        {
            _bookingCommand.Edit(value);
        }

        // DELETE api/<BookingController>/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            _bookingCommand.Delete(id);
        }
    }
}
