using AzureSearch.Services.ParentPage;
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
    [JobRegistration("0/5 * * * * ?")]
    public class RebuildIndexJob : IJob
    {
        private readonly ILogger<RebuildIndexJob> logger;
        private readonly IParentPageSearchService parentPageSearchService;

        public RebuildIndexJob(ILogger<RebuildIndexJob> logger,
            IParentPageSearchService parentPageSearchService)
        {
            this.logger = logger;
            this.parentPageSearchService = parentPageSearchService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            logger.LogInformation($"Job {nameof(RebuildIndexJob)} executed.");
            await parentPageSearchService.RebuildIndex();
            logger.LogInformation($"Index has been rebuilt.");
        }
    }
}
