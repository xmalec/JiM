using B2BWebApi.Models;
using BL.Models.User;

namespace BL.Services.Identity
{
    public interface IIdentityService : IService
    {
        IdentityModel GetIdentity(UserDto user);
    }
}
