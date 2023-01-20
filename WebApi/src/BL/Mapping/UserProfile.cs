using AutoMapper;
using BL.Models.User;
using DAL.Models;

namespace BL.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}
