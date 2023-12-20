using BL.Constants;
using BL.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BL.Services.EventLog
{
    public class EventLogLogger : ILogger
    {
        private readonly string name;
        private readonly Func<EventLogLoggerConfiguration> getCurrentConfig;
        private readonly IServiceProvider serviceProvider;

        public EventLogLogger(string source, Func<EventLogLoggerConfiguration> getCurrentConfig, IServiceProvider serviceProvider)
        {
            this.name = source;
            this.getCurrentConfig = getCurrentConfig;
            this.serviceProvider = serviceProvider;
        }

        public IDisposable? BeginScope<TState>(TState state) where TState : notnull => default!;

        public bool IsEnabled(LogLevel logLevel)
        {
            if (getCurrentConfig().LogLevel.TryGetValue(name, out var level) ||
                getCurrentConfig().LogLevel.TryGetValue(EventLogLoggerConfiguration.DEFAULT_KEY, out level))
            {
                return (level <= logLevel);
            }
            return false;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            Task.Run(async () =>
            {
                var serviceScopeFactory = serviceProvider.GetService<IServiceScopeFactory>();
                    if (serviceScopeFactory != null)
                    {
                        using (var scope = serviceScopeFactory.CreateScope())
                        {
                            var eventLogService = scope.ServiceProvider?.GetService<IEventLogService>();
                            if (eventLogService != null)
                            {
                            await eventLogService.Log(EventLogLevel.Convert(logLevel), state.ToString() ?? "Unknown message", name, eventId);
                            }
                        }
                    }
            });
        }
    }
}