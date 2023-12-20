using BL.Constants;
using BL.Options;
using DAL.Repositories;
using Infrastructure.UnitOfWorks;
using LinqKit;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BL.Services.EventLog
{
    public class EventLogService : IEventLogService
    {
        private readonly IBaseRepository<DAL.Models.EventLog> eventLogRepository;
        private readonly Func<IUnitOfWorkEventLog> unitOfWorkFactory;
        private readonly EventLogOptions eventLogOptions;

        public EventLogService(IBaseRepository<DAL.Models.EventLog> eventLogRepository, IOptions<EventLogOptions> options, Func<IUnitOfWorkEventLog> unitOfWorkFactory)
        {
            this.eventLogRepository = eventLogRepository;
            eventLogOptions = options.Value;
            this.unitOfWorkFactory = unitOfWorkFactory;
        }

        public async Task Log(EventLogLevel level, string message, string source, EventId eventId)
        {
            await InsertInternal(CreateEntityInternal(level, message, source, eventId, null));
        }

        public async Task Log(EventLogLevel level, string message, string source, Exception exception)
        {
            await InsertInternal(CreateEntityInternal(level, message, source, null, exception));
        }

        private async Task InsertInternal(DAL.Models.EventLog entity)
        {
            await eventLogRepository.Insert(entity);
        }

        private DAL.Models.EventLog CreateEntityInternal(EventLogLevel level, string message, string source, EventId? eventId, Exception? exception)
        {
            var entity = new DAL.Models.EventLog()
            {
                Source = source,
                Level = level.ToString(),
                Description = message,
            };
            if(eventId.HasValue)
            {
                entity.Event = eventId.Value.Name ?? string.Empty;
            }
            if(exception!= null)
            {
                entity.Exception = exception.Message;
                entity.CallStack = exception.StackTrace;
            }
            return entity;
        }

        public async Task CleanEventLog()
        {
            using (var uow = unitOfWorkFactory.Invoke())
            {
                var toDelete = CreateCleanQuery(uow.EventLogRepository.Query());
                foreach(var item in toDelete)
                {
                    await uow.EventLogRepository.Delete(item);
                }
                await uow.Commit();
            }
        }

        private IQueryable<DAL.Models.EventLog> CreateCleanQuery(IQueryable<DAL.Models.EventLog> query)
        {
            var predicate = PredicateBuilder.New<DAL.Models.EventLog>();

            foreach (var logLevel in Enum.GetValues<LogLevel>())
            {
                var hours = eventLogOptions.Lifetime.GetValueOrDefault(logLevel, 1);
                predicate = predicate.Or(log => EventLogLevel.Convert(logLevel).Level == log.Level
                && (!log.CreatedDate.HasValue || log.CreatedDate.Value.AddHours(hours) <= DateTime.Now));
            }
            return query.Where(predicate);
        }
    }
}
