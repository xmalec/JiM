using BL.Services.EventLog;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebApi.DI;

namespace WebApi.Logging
{
    [ProviderAlias("EventLog")]
    public class EventLogLoggerProvider : ILoggerProvider
    {
        private readonly IServiceProvider serviceProvider;

        public EventLogLoggerProvider(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new EventLogLogger(serviceProvider.GetRequiredService<IEventLogService>());
        }

        public void Dispose()
        {
            return;
        }
    }
}