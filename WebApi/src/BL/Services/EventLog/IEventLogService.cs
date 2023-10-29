using BL.Constants;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.EventLog
{
    public interface IEventLogService : IService
    {
        Task Log(EventLogLevel level, string message, string source);
        Task Log(EventLogLevel level, string message, EventId eventId);
        Task Log(EventLogLevel level, string message, string source, Exception? exception);
    }
}
