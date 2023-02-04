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
    //[JobRegistration("0/3 * * * * ?")]
    public class HelloJob : IJob
    {
        private readonly ILogger<HelloJob> logger;

        public HelloJob(ILogger<HelloJob> logger)
        {
            this.logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            logger.LogInformation($"Job {nameof(HelloJob)} executed.");
        }
    }
}
