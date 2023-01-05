using BL.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.User
{
    public interface IUserService : IService
    {
        Task<UserDto> LoginUser(string email, string password);
    }
}
