using B2BWebApi.Models;
using BL.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.Identity
{
    public interface IIdentityService : IService
    {
        IdentityModel GetIdentity(UserDto user);
    }
}
