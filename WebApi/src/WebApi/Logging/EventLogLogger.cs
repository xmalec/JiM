using BL.Constants;
using BL.DI;
using BL.Services.EventLog;

namespace WebApi.Logging
{
    public class EventLogLogger : ILogger
    {

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
            
            //eventLogService.Log(EventLogLevel.Convert(logLevel), state.ToString() ?? "Unknown message", eventId);
                // Use eventLogService within the scope.
            
        }
    }
}