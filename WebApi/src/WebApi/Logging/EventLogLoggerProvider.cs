using BL.DI;
using BL.Services.EventLog;
using System.Collections.Concurrent;

namespace WebApi.Logging
{
    [ProviderAlias("EventLog")]
    public class EventLogLoggerProvider : ILoggerProvider
    {
        private readonly IServiceScopeFactory scopeFactory;
        private readonly ConcurrentDictionary<string, EventLogLogger> _loggers = new(StringComparer.OrdinalIgnoreCase);

        public EventLogLoggerProvider(IServiceScopeFactory scopeFactory)
        {
            this.scopeFactory = scopeFactory;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return _loggers.GetOrAdd(categoryName, name => new EventLogLogger());
        }

        public void Dispose()
        {
            _loggers.Clear();
            return;
        }
    }
}