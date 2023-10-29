using Microsoft.Extensions.Logging;

namespace BL.Services.EventLog
{
    [ProviderAlias("EventLog")]
    public class EventLogLoggerProvider : ILoggerProvider
    {
        private readonly EventLogLogger _logger;
        private readonly IEventLogService eventLogService;

        public EventLogLoggerProvider(IEventLogService eventLogService)
        {
            this.eventLogService = eventLogService;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new EventLogLogger(eventLogService);
        }

        public void Dispose()
        {
            return;
        }
    }
}