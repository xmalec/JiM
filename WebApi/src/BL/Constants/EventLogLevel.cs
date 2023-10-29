using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Constants
{
    public class EventLogLevel
    {
        public string Level { get; set; }

        private EventLogLevel(string level)
        {
            Level = level;
        }

        public static EventLogLevel TRACE = new EventLogLevel("TRACE");
        public static EventLogLevel DEBUG = new EventLogLevel("DEBUG");
        public static EventLogLevel INFO = new EventLogLevel("INFORMATION");
        public static EventLogLevel WARNING = new EventLogLevel("WARNING");
        public static EventLogLevel ERROR = new EventLogLevel("ERROR");
        public static EventLogLevel CRITICAL = new EventLogLevel("CRITICAL");
        public static EventLogLevel NONE = new EventLogLevel("NONE");

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
            return obj is EventLogLevel type &&
                   Level == type.Level;
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
