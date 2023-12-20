using Microsoft.Extensions.Logging;

namespace BL.Constants
{
    public class EventLogLevel
    {
        public string Level { get; set; }

        private EventLogLevel(string level)
        {
            Level = level;
        }

        public static EventLogLevel TRACE = new EventLogLevel("Trace");
        public static EventLogLevel DEBUG = new EventLogLevel("Debug");
        public static EventLogLevel INFO = new EventLogLevel("Information");
        public static EventLogLevel WARNING = new EventLogLevel("Warning");
        public static EventLogLevel ERROR = new EventLogLevel("Error");
        public static EventLogLevel CRITICAL = new EventLogLevel("Critical");
        public static EventLogLevel NONE = new EventLogLevel("None");

        public static EventLogLevel Convert(LogLevel logLevel)
        {
            return (logLevel) switch
            {
                LogLevel.Trace => TRACE,
                LogLevel.Debug => DEBUG,
                LogLevel.Information => INFO,
                LogLevel.Warning => WARNING,
                LogLevel.Error => ERROR,
                LogLevel.Critical => CRITICAL,
                _ => NONE,
            };
        }

        public override bool Equals(object? obj)
        {
            if(obj is EventLogLevel type)
            {
                return Level == type.Level;
            }
            if(obj is string str)
            {
                return Level.ToLower() == str.ToLower();
            }
            return false;       
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Level);
        }

        public override string ToString()
        {
            return Level;
        }
    }
}
