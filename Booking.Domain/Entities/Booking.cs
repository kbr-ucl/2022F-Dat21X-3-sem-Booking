using Booking.Domain.DomainServices;
using Microsoft.Extensions.DependencyInjection;

namespace Booking.Domain.Entities;

public class Booking
{
    private readonly IServiceProvider _serviceProvider;

    // DO NOT USE TEST ONLY
    protected Booking(DateTime start, DateTime slut)
    {
        Start = start;
        Slut = slut;
    }

    public Booking(IServiceProvider serviceProvider, DateTime start, DateTime slut)
    {
        if (start == default) throw new ArgumentOutOfRangeException(nameof(start),"Start dato skal være udfyldt");
        if (slut == default) throw new ArgumentOutOfRangeException(nameof(slut), "Slut dato skal være udfyldt");
        if (start >= slut)
            throw new Exception($"Slut dato/tid skal være senere end start (start, slut): {start}, {slut}");
        Start = start;
        Slut = slut;
        _serviceProvider = serviceProvider;

        if (IsOverlapping()) throw new Exception("Booking overlapper med eksisterende booking");
    }

    public Guid Id { get; set; }
    public DateTime Start { get; private set; }
    public DateTime Slut { get; private set; }

    protected bool IsOverlapping()
    {
        var bookingDomainService = _serviceProvider.GetService<IBookingDomainService>();
        if (bookingDomainService == null) throw new Exception("Implementation of IBookingDomainService was not found");

        return bookingDomainService.GetExsistingBookings()
            .Any(a => a.Start >= Start && a.Start <= Slut || a.Slut >= Start && a.Slut <= Slut);
    }
}