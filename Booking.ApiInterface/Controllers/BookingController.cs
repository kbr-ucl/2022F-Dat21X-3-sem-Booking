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
    public async Task<IEnumerable<BookingDto>> GetAsync()
    {
        var result = new List<BookingDto>();
        var bookings = await _bookingQuery.GetBookingsAsync();
        bookings.ToList()
            .ForEach(a => result.Add(new BookingDto {Id = a.Id, Slut = a.Slut, Start = a.Start, Version = a.Version}));
        return result;
    }

    // GET api/<BookingController>/5
    [HttpGet("{id}")]
    public async Task<BookingDto?> GetAsync(Guid id)
    {
        var booking = await _bookingQuery.GetBookingAsync(id);
        if (booking is null) return null;
        return new BookingDto {Id = booking.Id, Slut = booking.Slut, Start = booking.Start, Version = booking.Version};
    }

    // POST api/<BookingController>
    [HttpPost]
    public async Task CreateAsync([FromBody] BookingDto value)
    {
        await _bookingCommand.CreateAsync(new BookingCommandDto {Id = value.Id, Slut = value.Slut, Start = value.Start});
    }

    // PUT api/<BookingController>/5
    [HttpPut]
    public async Task EditAsync([FromBody] BookingDto value)
    {
            await _bookingCommand.EditAsync(new BookingCommandDto { Id = value.Id, Slut = value.Slut, Start = value.Start, Version = value.Version });
    }

    // DELETE api/<BookingController>/5
    [HttpDelete("{id}")]
    public async Task DeleteAsync(Guid id)
    {
        await _bookingCommand.DeleteAsync(new BookingCommandDto {Id = id});
    }
}