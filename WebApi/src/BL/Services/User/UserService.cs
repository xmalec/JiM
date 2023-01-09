using AutoMapper;
using BL.Models.User;
using DAL.Repositories;
using Extensions.Extensions;

namespace BL.Services.User
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        public async Task<UserDto?> LoginUser(string email, string password)
        {
            var user = userRepository.GetUserByEmail(email);
            if (user != null && BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                return user.Map<UserDto>(mapper);
            }
            return null;
        }
    }
}
