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

        public async Task Log(EventLogLevel level, string message, string source)
        {
            await InsertInternal(CreateEntityInternal(level, message, source, null));
        }

        public async Task Log(EventLogLevel level, string message, string source, Exception? exception)
        {
            await InsertInternal(CreateEntityInternal(level, message, source, exception));
        }

        public async Task Log(EventLogLevel level, string message, EventId eventId)
        {
            await InsertInternal(CreateEntityInternal(level, message, eventId.Name ?? "UNKNOWN Source", null));
        }

        private async Task InsertInternal(DAL.Models.EventLog entity)
        {
            await eventLogRepository.Insert(entity);
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
