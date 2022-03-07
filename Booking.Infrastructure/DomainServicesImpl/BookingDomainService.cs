using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booking.Domain.DomainServices;

namespace Booking.Infrastructure.DomainServicesImpl
{
    public class BookingDomainService : IBookingDomainService
    {
        IEnumerable<Domain.Entities.Booking> IBookingDomainService.GetExsistingBookings()
        {
            throw new NotImplementedException();
        }
    }
}
