using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleTasks.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class JobRegistrationAttribute : Attribute
    {
        public string CronExpr { get; }

        public JobRegistrationAttribute(string cronExpr)
        {
            if(!CronExpression.IsValidExpression(cronExpr))
            {
                throw new ArgumentException($"Invalid cron expression {cronExpr}");
            }
            this.CronExpr = cronExpr;
        }
    }
}
