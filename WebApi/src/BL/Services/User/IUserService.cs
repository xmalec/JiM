using BL.Models.User;

namespace BL.Services.User
{
    public interface IUserService : IBaseCRUDService<DAL.Models.User, UserDto>, IService
    {
        Task<UserDto> LoginUser(string email, string password);
        Task<bool> IsEmailUnique(string email);
    }
}
