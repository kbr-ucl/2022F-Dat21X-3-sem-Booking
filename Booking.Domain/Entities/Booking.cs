using Booking.Domain.DomainServices;
using Microsoft.Extensions.DependencyInjection;

namespace Booking.Domain.Entities;

public class Booking
{
    public IServiceProvider? ServiceProvider { get; set; }

    public Guid Id { get; }
    public DateTime Start { get; private set; }
    public DateTime Slut { get; private set; }
    public byte[] Version { get; private set; }

    public Booking(IServiceProvider serviceProvider, DateTime start, DateTime slut)
    {
        if (start == default) throw new ArgumentOutOfRangeException(nameof(start), "Start dato skal være udfyldt");
        if (slut == default) throw new ArgumentOutOfRangeException(nameof(slut), "Slut dato skal være udfyldt");
        if (start >= slut)
            throw new Exception($"Slut dato/tid skal være senere end start (start, slut): {start}, {slut}");
        Start = start;
        Slut = slut;
        ServiceProvider = serviceProvider;

        if (IsOverlapping()) throw new Exception("Booking overlapper med eksisterende booking");
        Id = Guid.NewGuid();
    }

    // DO NOT USE - TEST ONLY!!!
    public Booking(Guid id, DateTime start, DateTime slut)
    {
        Id = id;
        Start = start;
        Slut = slut;
    }


    protected virtual bool IsOverlapping()
    {
        var bookingDomainService = ServiceProvider?.GetService<IBookingDomainService>();
        if (bookingDomainService == null) throw new Exception("Implementation of IBookingDomainService was not found");

        return bookingDomainService.GetOtherBookings(this)
            // https://stackoverflow.com/questions/325933/determine-whether-two-date-ranges-overlap
            .Any(a => a.Id != Id && a.Start <= Slut && Start <= a.Slut);
    }

    public void Update(DateTime start, DateTime slut, byte[] version)
    {
        if (start == default) throw new ArgumentOutOfRangeException(nameof(start), "Start dato skal være udfyldt");
        if (slut == default) throw new ArgumentOutOfRangeException(nameof(slut), "Slut dato skal være udfyldt");
        if (start >= slut)
            throw new Exception($"Slut dato/tid skal være senere end start (start, slut): {start}, {slut}");

        Start = start;
        Slut = slut;
        Version = version;
        if (IsOverlapping()) throw new Exception("Booking overlapper med eksisterende booking");
    }
}