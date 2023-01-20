using AutoMapper;
using BL.Models.User;
using DAL.Repositories;
using Extensions.Extensions;

namespace BL.Services.User
{
    public class UserService : BaseCRUDService<DAL.Models.User, UserDto>, IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository) : base(userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<bool> IsEmailUnique(string email)
        {
            return !userRepository.Query().Any(x => x.Email == email);
        }

        public async Task<UserDto?> LoginUser(string email, string password)
        {
            var user = userRepository.GetUserByEmail(email);
            if (user != null && BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                return user.Map<UserDto>();
            }
            return null;
        }
    }
}
