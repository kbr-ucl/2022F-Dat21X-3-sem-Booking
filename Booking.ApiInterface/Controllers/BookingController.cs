using Booking.Application.Contract;
using Booking.Application.Contract.Dtos;
using Booking.Contract;
using Booking.Contract.Dtos;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Booking.ApiInterface.Controllers;

[Route("api/Booking")]
[ApiController]
public class BookingController : ControllerBase, IBookingService
{
    private readonly IBookingCommand _bookingCommand;
    private readonly IBookingQuery _bookingQuery;

    public BookingController(IBookingQuery bookingQuery, IBookingCommand bookingCommand)
    {
        _bookingQuery = bookingQuery;
        _bookingCommand = bookingCommand;
    }

    // GET: api/<BookingController>
    /// <summary>
    ///     Her kan man skrive om GET
    /// </summary>
    [HttpGet]
    public IEnumerable<BookingDto> Get()
    {
        var result = new List<BookingDto>();
        _bookingQuery.GetBookings().ToList()
            .ForEach(a => result.Add(new BookingDto {Id = a.Id, Slut = a.Slut, Start = a.Start, Version = a.Version}));
        return result;
    }

    // GET api/<BookingController>/5
    [HttpGet("{id}")]
    public BookingDto? Get(Guid id)
    {
        var booking = _bookingQuery.GetBooking(id);
        if (booking is null) return null;
        return new BookingDto {Id = booking.Id, Slut = booking.Slut, Start = booking.Start, Version = booking.Version };
    }

    // POST api/<BookingController>
    [HttpPost]
    public void Create([FromBody] BookingDto value)
    {
        _bookingCommand.Create(new BookingCommandDto {Id = value.Id, Slut = value.Slut, Start = value.Start, Version = value.Version});
    }

    // PUT api/<BookingController>/5
    [HttpPut]
    public void Edit([FromBody] BookingDto value)
    {
        _bookingCommand.Edit(new BookingCommandDto {Id = value.Id, Slut = value.Slut, Start = value.Start, Version = value.Version });
    }

    // DELETE api/<BookingController>/5
    [HttpDelete]
    public void Delete(BookingDto value)
    {
        _bookingCommand.Delete(new BookingCommandDto {Id = value.Id, Slut = value.Slut, Start = value.Start, Version = value.Version });
    }
}