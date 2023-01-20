using AutoMapper;
using BL.Models.User;
using DAL.Models;
using WebApi.Models.User;

namespace WebApi.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserCreateModel, UserDto>()
                .ForMember(u => u.PasswordHash, opt => opt.MapFrom(x => BCrypt.Net.BCrypt.HashPassword(x.Password)))
                .ReverseMap();
        }
    }
}
