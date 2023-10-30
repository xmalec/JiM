using Microsoft.Extensions.Logging;

namespace BL.Options
{
    public class EventLogLoggerConfiguration
    {
        public static string DEFAULT_KEY = "Default";
        public Dictionary<string, LogLevel> LogLevel { get; set; } = new()
        {
            { DEFAULT_KEY, Microsoft.Extensions.Logging.LogLevel.Error }
        };
    }
}
