namespace Booking.Application.Contract.Dtos;

public class BookingQueryDto
{
    public Guid Id { get; set; }
    public DateTime Start { get; set; }
    public DateTime Slut { get; set; }
    public byte[] Version { get; set; }
}