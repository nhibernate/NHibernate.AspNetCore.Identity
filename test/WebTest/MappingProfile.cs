using AutoMapper;
using WebTest.Entities;
using WebTest.Models;

namespace WebTest {

    public class MappingProfile : Profile {

        public MappingProfile() {
            CreateMap<TodoItem, TodoItemModel>()
                .ForMember(
                    dest => dest.UserId,
                    map => map.MapFrom(e => e.User.Id)
                )
                .ForMember(
                    d => d.UserName,
                    m => m.MapFrom(e => e.User.UserName)
                )
                .ReverseMap()
                .ForMember(
                    d => d.User,
                    m => m.MapFrom(s => new AppUser { Id = s.UserId })
                );
        }

    }

}
