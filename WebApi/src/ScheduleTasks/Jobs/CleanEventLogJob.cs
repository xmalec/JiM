using AzureSearch.Services.ParentPage;
using BL.Services.EventLog;
using BL.Services.ScheduleTask;
using Microsoft.Extensions.Logging;
using Quartz;
using ScheduleTasks.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleTasks.Jobs
{
    //[JobRegistration("0/5 0 0/4 ? * * *")]
    [JobRegistration("0/5 * * * * ?")]
    public class CleanEventLogJob : DbJob
    {
        private readonly ILogger<CleanEventLogJob> logger;
        private readonly IEventLogService eventLogService;

        public CleanEventLogJob(IScheduleTaskService scheduleTaskService,
            ILogger<CleanEventLogJob> logger,
            IEventLogService eventLogService) : base(scheduleTaskService, logger)
        {
            this.logger = logger;
            this.eventLogService = eventLogService;
        }

        internal override string JobName => nameof(CleanEventLogJob);

        internal override async Task RunJob()
        {
            logger.LogInformation($"Schedule task {nameof(CleanEventLogJob)} executed.");
            await eventLogService.CleanEventLog();
            logger.LogInformation($"Index has been rebuilt.");
        }
    }
}
