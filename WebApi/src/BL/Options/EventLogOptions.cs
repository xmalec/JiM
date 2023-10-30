using Microsoft.Extensions.Logging;

namespace BL.Options
{
    public class EventLogOptions
    {
        public const string SectionName = "EventLog";

        public int MaxSize { get; set; } = 1000;
        public Dictionary<LogLevel, int> Lifetime { get; set; } = new()
        {
            {LogLevel.Trace, 1 },
            {LogLevel.Debug, 1 },
            {LogLevel.Information, 3 },
            {LogLevel.Warning, 24 },
            {LogLevel.Error, 48 },
            {LogLevel.Critical, 72 },
        };
    }
}
