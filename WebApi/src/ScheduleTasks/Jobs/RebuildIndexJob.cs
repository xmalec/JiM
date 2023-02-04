using AzureSearch.Services.ParentPage;
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
    //[JobRegistration("0/5 * * * * ?")]
    public class RebuildIndexJob : DbJob
    {
        private readonly ILogger<RebuildIndexJob> logger;
        private readonly IParentPageSearchService parentPageSearchService;

        public RebuildIndexJob(IScheduleTaskService scheduleTaskService,
            ILogger<RebuildIndexJob> logger,
            IParentPageSearchService parentPageSearchService) : base(scheduleTaskService, logger)
        {
            this.logger = logger;
            this.parentPageSearchService = parentPageSearchService;
        }

        internal override string JobName => nameof(RebuildIndexJob);

        internal override async Task RunJob()
        {
            logger.LogInformation($"Schedule task {nameof(RebuildIndexJob)} executed.");
            await parentPageSearchService.RebuildIndex();
            logger.LogInformation($"Index has been rebuilt.");
        }
    }
}
