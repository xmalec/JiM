using BL.DI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BL.Services.EventLog
{
    [ProviderAlias("EventLog")]
    public class EventLogLoggerProvider : ILoggerProvider
    {

        public ILogger CreateLogger(string categoryName)
        {
            return new EventLogLogger();
        }

        public void Dispose()
        {
            return;
        }
    }
}