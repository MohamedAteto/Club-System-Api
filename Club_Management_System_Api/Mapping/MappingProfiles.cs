
using AutoMapper;
using Club_Management_System_Api.Models; // <-- Make sure this is added
using Club_Management_System_Api.DTOs.MemberDTOs;
using Club_Management_System_Api.DTOs.MemberShipDTOs;
using Club_Management_System_Api.DTOs.FacilityDTOs;
using Club_Management_System_Api.DTOs.BookingDTOs;
using Club_Management_System_Api.DTOs.PaymentDTOs;

namespace Club_Management_System_Api.Mapping
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {

            CreateMap<Member, MemberDTO>().ReverseMap();
            CreateMap<Member, CreateMemberDTO>().ReverseMap();
            CreateMap<Member, UpdateMemberDTO>().ReverseMap();

            CreateMap<MemberShip,MemberShipDTO>().ReverseMap();
            CreateMap<MemberShip,CreateMemberShipDTO>().ReverseMap();
            CreateMap<MemberShip,UpdateMemberShipDTO>().ReverseMap();


            CreateMap<Facility , FacilityDTO>().ReverseMap();
            CreateMap<Facility , CreateFacilityDTO>().ReverseMap();


            CreateMap<Booking,BookingDTO>().ReverseMap();
            CreateMap<Booking,CreateBookingDTO>().ReverseMap();
            CreateMap<Booking,UpdateBookingDTO>().ReverseMap();


            CreateMap<Payment, PaymentStatisticsDTO>().ReverseMap();
            CreateMap<Payment,CreatePaymentDTO>().ReverseMap();

        }
    }
}



