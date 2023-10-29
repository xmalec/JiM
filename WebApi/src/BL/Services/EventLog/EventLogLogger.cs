using BL.Constants;
using Microsoft.Extensions.Logging;

namespace BL.Services.EventLog
{
    public class EventLogLogger : ILogger
    {
        private readonly IEventLogService eventLogService;

        public EventLogLogger(IEventLogService eventLogService)
        {
            this.eventLogService = eventLogService;
        }

        public IDisposable? BeginScope<TState>(TState state) where TState : notnull
        {
            throw new NotImplementedException();
        }

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
            eventLogService.Log(EventLogLevel.Convert(logLevel), state.ToString() ?? "Unknown message", eventId);
        }
    }
}