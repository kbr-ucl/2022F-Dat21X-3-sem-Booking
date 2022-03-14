namespace Booking.Application.Infrastructure;

public interface IBookingRepository
{
    void Add(Domain.Entities.Booking booking);
    Domain.Entities.Booking Get(Guid id);
    void Save(Domain.Entities.Booking booking);
    void Delete(Guid id);
}