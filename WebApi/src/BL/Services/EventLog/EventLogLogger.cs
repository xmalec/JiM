using BL.Constants;
using BL.DI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BL.Services.EventLog
{
    public class EventLogLogger : ILogger
    {
        private IEventLogService? _eventLogService;

        private IEventLogService EventLogService
        {
            get
            {
                if(_eventLogService == null)
                {
                    _eventLogService = ServiceFactory.Current.ServiceProvider.GetService<IEventLogService>();
                }
                return _eventLogService;
            }
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
            EventLogService.Log(EventLogLevel.Convert(logLevel), state.ToString() ?? "Unknown message", eventId);
        }
    }
}