using BL.Constants;
using BL.DI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BL.Services.EventLog
{
    public class EventLogLogger : ILogger
    {
        private readonly string source;

        public EventLogLogger(string source)
        {
            this.source = source;
        }

        public IDisposable? BeginScope<TState>(TState state) where TState : notnull => default!;

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            Task.Run(async () =>
            {
                var serviceScopeFactory = ServiceFactory.Current.ServiceProvider?.GetRequiredService<IServiceScopeFactory>();
                if (serviceScopeFactory != null)
                {
                    using (var scope = serviceScopeFactory.CreateScope())
                    {
                        var eventLogService = scope.ServiceProvider?.GetService<IEventLogService>();
                        if (eventLogService != null)
                        {
                            await eventLogService.Log(EventLogLevel.Convert(logLevel), state.ToString() ?? "Unknown message", source, eventId);
                        }
                    }
                }
            });
        }
    }
}