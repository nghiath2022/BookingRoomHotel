using AutoMapper;
using BookingRoom.DTOs;
using BookingRoom.Models;

namespace BookingRoom.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // User
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.Name));

            CreateMap<UserDto, User>();

            // Room
            CreateMap<Room, RoomDto>()
                .ForMember(dest => dest.RoomTypeName, opt => opt.MapFrom(src => src.RoomType.TypeName));

            CreateMap<RoomDto, Room>();

            // Booking, Payment: bạn có thể thêm tiếp ở đây
            CreateMap<Booking, BookingDto>()
                    .ForMember(dest => dest.UserFullName, opt => opt.MapFrom(src => src.User.FullName))
                    .ForMember(dest => dest.RoomName, opt => opt.MapFrom(src => src.Room.Name));

            CreateMap<BookingCreateDto, Booking>();
            CreateMap<BookingUpdateDto, Booking>();

            //Payment
            CreateMap<Payment, PaymentDto>()
                    .ForMember(dest => dest.BookingCode, opt => opt.MapFrom(src => src.Booking.Id.ToString()));

            CreateMap<PaymentCreateDto, Payment>();
            CreateMap<PaymentUpdateDto, Payment>();

            //RoomType
            CreateMap<RoomType, RoomTypeDto>();
            CreateMap<RoomTypeDto, RoomType>();
        }
    }
}
