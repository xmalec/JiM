using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.Setting
{
    public interface ISettingService : IService
    {
        string GetString(string key);
        int GetInteger(string key);
        bool GetBoolean(string key);
    }
}
