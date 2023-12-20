using BL.Constants;
using Microsoft.Extensions.Logging;

namespace BL.Services.EventLog
{
    public interface IEventLogService : IService
    {
        Task CleanEventLog();
        Task Log(EventLogLevel level, string message, string source, EventId eventId);
        Task Log(EventLogLevel level, string message, string source, Exception exception);
    }
}
