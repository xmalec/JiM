using BL.Constants;
using DAL.Repositories;
using Microsoft.Extensions.Logging;
using System;

namespace BL.Services.EventLog
{
    public class EventLogService : IEventLogService
    {
        private readonly IBaseRepository<DAL.Models.EventLog> eventLogRepository;

        public EventLogService(IBaseRepository<DAL.Models.EventLog> eventLogRepository)
        {
            this.eventLogRepository = eventLogRepository;
        }

        public void Log(EventLogLevel level, string message, string source)
        {
            InsertInternal(CreateEntityInternal(level, message, source, null));
        }

        public void Log(EventLogLevel level, string message, string source, Exception? exception)
        {
            InsertInternal(CreateEntityInternal(level, message, source, exception));
        }

        public void Log(EventLogLevel level, string message, EventId eventId)
        {
            InsertInternal(CreateEntityInternal(level, message, eventId.Name ?? "UNKNOWN Source", null));
        }

        private void InsertInternal(DAL.Models.EventLog entity)
        {
            eventLogRepository.Insert(entity);
        }

        private DAL.Models.EventLog CreateEntityInternal(EventLogLevel level, string message, string source, Exception? exception)
        {
            var entity = new DAL.Models.EventLog()
            {
                Source = source,
                Level = level.ToString(),
                Description = message
            };
            if(exception!= null)
            {
                entity.Exception = exception.Message;
            }
            return entity;
        }
    }
}
