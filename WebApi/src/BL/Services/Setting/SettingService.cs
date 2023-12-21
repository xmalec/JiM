using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace BL.Services.Setting
{
    public class SettingService : ISettingService
    {
        private readonly IConfiguration configuration;
        private readonly ILogger<SettingService> logger;

        public SettingService(IConfiguration configuration, ILogger<SettingService> logger)
        {
            this.configuration = configuration;
            this.logger = logger;
        }

        public bool GetBoolean(string key)
        {
            if (Validate(key, out var value))
            {
                if (bool.TryParse(value, out var result))
                {
                    return result;
                }
                logger.LogError("Cannot get boolean value of setting {0}! Application setting is not an boolean.", key);
                throw new ArgumentException($"Cannot get boolean value of setting {key}! Application setting is not an boolean.");
            }
            return default;
        }

        public string GetString(string key)
        {
            if (Validate(key, out var value))
            {
                return value;
            }
            return string.Empty;
        }

        public int GetInteger(string key)
        {
            if(Validate(key, out var value))
            {
                if (int.TryParse(value, out var result))
                {
                    return result;
                }
                logger.LogError("Cannot get integer value of setting {0}! Application setting is not an integer.", key);
                throw new ArgumentException($"Cannot get integer value of setting {key}! Application setting is not an integer.");
            }
            return default;
        }

        private bool Validate(string key, out string value)
        {
            if(string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("Invalid key.");
            }
            if (!GetSetting(key, out value))
            {
                logger.LogError("Application setting {0} is not set!", key);
                throw new ArgumentException($"Application setting {key} is not set!");
            }
            return true;
        }

        private bool GetSetting(string key, out string res)
        {
            res = configuration[key] ?? string.Empty;
            return !string.IsNullOrEmpty(res);
        }

        
    }
}
