using System;
using AutoMapper;
using WebTest.Entities;
using WebTest.Models;

namespace WebTest.AutoMapper {
    public class DtoToDoMappingProfile : Profile {
        public DtoToDoMappingProfile() {
            CreateMap<UserModel, ApplicationUser>()
                .ForMember(dest => dest.Email, m => m.MapFrom(src => src.email))
                .ForMember(dest => dest.UserName, m => m.MapFrom(src => src.username))
                .ForMember(dest => dest.PhoneNumber, m => m.MapFrom(src => src.phoneNumber))
                .ForMember(dest => dest.CreateTime, m => m.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.EmailConfirmed, m => m.MapFrom(src => false))
                .ForMember(dest => dest.LockoutEnabled, m => m.MapFrom(src => true))
                .ForMember(dest => dest.TwoFactorEnabled, m => m.MapFrom(src => false));
        }
    }
}
