using BL.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Concurrent;

namespace BL.Services.EventLog
{
    [ProviderAlias("DbEventLog")]
    public class EventLogLoggerProvider : ILoggerProvider
    {
        private readonly IDisposable? onChangeToken;
        private EventLogLoggerConfiguration currentConfig;
        private readonly IServiceProvider serviceProvider;
        private readonly ConcurrentDictionary<string, EventLogLogger> _loggers = new(StringComparer.OrdinalIgnoreCase);

        public EventLogLoggerProvider(IOptionsMonitor<EventLogLoggerConfiguration> config, IServiceProvider serviceProvider)
        {
            currentConfig = config.CurrentValue;
            onChangeToken = config.OnChange(updatedConfig => currentConfig = updatedConfig);
            this.serviceProvider = serviceProvider;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return _loggers.GetOrAdd(categoryName, name => new EventLogLogger(categoryName, GetCurrentConfig, serviceProvider));
        }

        private EventLogLoggerConfiguration GetCurrentConfig() => currentConfig;

        public void Dispose()
        {
            _loggers.Clear();
            onChangeToken?.Dispose();
        }
    }
}