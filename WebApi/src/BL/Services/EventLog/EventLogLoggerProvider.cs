using BL.DI;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace BL.Services.EventLog
{
    [ProviderAlias("EventLog")]
    public class EventLogLoggerProvider : ILoggerProvider
    {
        private readonly ConcurrentDictionary<string, EventLogLogger> _loggers = new(StringComparer.OrdinalIgnoreCase);

        public ILogger CreateLogger(string categoryName)
        {
            return _loggers.GetOrAdd(categoryName, name => new EventLogLogger(categoryName));
        }

        public void Dispose()
        {
            _loggers.Clear();
            return;
        }
    }
}