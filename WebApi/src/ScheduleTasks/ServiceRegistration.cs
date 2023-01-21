using BL.Options;
using BL.Services;
using Extensions.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz.Impl;
using Quartz;
using System.Net.Http;
using System.Reflection;
using Microsoft.Extensions.Options;
using Quartz.Impl.Calendar;
using Quartz.Impl.Matchers;
using System.Globalization;
using ScheduleTasks.Jobs;

namespace ScheduleTasks
{
    public static class ServiceRegistration
    {
        public static IServiceCollection InitScheduleTasks(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<QuartzOptions>(options =>
            {
                options.Scheduling.IgnoreDuplicates = true;
                options.Scheduling.OverWriteExistingData = true;
            });

            services.AddQuartz(q =>
            {
                // handy when part of cluster or you want to otherwise identify multiple schedulers
                q.SchedulerId = "Scheduler-Core";
                q.UseMicrosoftDependencyInjectionJobFactory();
                q.UseSimpleTypeLoader();
                q.UseInMemoryStore();
                q.UseDefaultThreadPool(tp =>
                {
                    tp.MaxConcurrency = 10;
                });
                var a = DateBuilder.EvenSecondDate(DateTimeOffset.UtcNow.AddSeconds(15));
                // quickest way to create a job with single trigger is to use ScheduleJob
                // (requires version 3.2)
                q.ScheduleJob<HelloJob>(trigger => trigger
                    .WithIdentity("Combined Configuration Trigger")
                    .StartAt(DateBuilder.EvenSecondDate(DateTimeOffset.UtcNow.AddSeconds(7)))
                    .WithDailyTimeIntervalSchedule(x => x.WithInterval(10, IntervalUnit.Second))
                    .WithDescription("my awesome trigger configured for a job with single call")
                );

            });
            services.AddTransient<HelloJob>();
            return services;
        }
    }
}
