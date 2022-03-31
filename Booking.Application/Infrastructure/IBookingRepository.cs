using Booking.Application.Contract.Dtos;

namespace Booking.Application.Infrastructure;

public interface IBookingRepository
{
    Task AddAsync(Domain.Entities.Booking booking);
    Task<Domain.Entities.Booking> GetAsync(Guid id);
    Task SaveAsync(Domain.Entities.Booking booking);
    Task DeleteAsync(Guid id);
}