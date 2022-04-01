using AutoMapper;
using Booking.Contract.Dtos;
using Booking.Web.Pages.Booking;

namespace Booking.Web.Mappers
{
    public class BookingProfile : Profile
    {
        public BookingProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<BookingDto, IndexModel.BookingIndexModel>();
            CreateMap<BookingDto, DetailsModel.BookingDetailsModel>();
            CreateMap<BookingDto, EditModel.BookingEditModel>().ReverseMap();
            CreateMap<BookingDto, DeleteModel.BookingDeleteModel>();
            CreateMap<BookingDto, IndexModel.BookingIndexModel>();

            CreateMap<CreateModel.BookingCreateModel, BookingDto>();
        }
    }
}
