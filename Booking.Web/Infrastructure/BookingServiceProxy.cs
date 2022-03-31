using System.Net.Mime;
using System.Text;
using System.Text.Json;
using Booking.Contract;
using Booking.Contract.Dtos;

namespace Booking.Web.Infrastructure
{
    public class BookingServiceProxy : IBookingService
    {
        private readonly HttpClient _client;

        public BookingServiceProxy(HttpClient client)
        {
            _client = client;
        }

        async Task IBookingService.CreateAsync(BookingDto bookingDto)
        {
            var bookingDtoJson = new StringContent(
                JsonSerializer.Serialize(bookingDto),
                Encoding.UTF8,
                MediaTypeNames.Application.Json);
            await _client.PostAsync("/api/Booking", bookingDtoJson);
        }

        async Task IBookingService.DeleteAsync(Guid id)
        {

            await _client.DeleteAsync($"/api/Booking/{id}");
        }

        async Task IBookingService.EditAsync(BookingDto bookingDto)
        {
            var bookingDtoson = new StringContent(
                JsonSerializer.Serialize(bookingDto),
                Encoding.UTF8,
                MediaTypeNames.Application.Json);
            await _client.PutAsync("/api/Booking", bookingDtoson);
        }

        async Task<BookingDto?> IBookingService.GetAsync(Guid id)
        {
            return await _client.GetFromJsonAsync<BookingDto?>($"api/Booking/{id}");
        }

        async Task<IEnumerable<BookingDto>> IBookingService.GetAsync()
        {
            return await _client.GetFromJsonAsync<IEnumerable<BookingDto>>($"api/Booking");
        }
    }
}
