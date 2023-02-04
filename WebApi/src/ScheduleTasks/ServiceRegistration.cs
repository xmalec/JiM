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
using ScheduleTasks.Attributes;

namespace ScheduleTasks
{
    public static class ServiceRegistration
    {
        public static IServiceCollection SetupScheduleTasks(this IServiceCollection services, IConfiguration configuration)
        {
            var jobs = GetJobs().ToList();
            if(jobs.Any())
            {
                services.AddQuartz(q =>
                {
                    q.SchedulerId = "JiM schedule tasks";
                    q.UseMicrosoftDependencyInjectionJobFactory();
                    q.UseSimpleTypeLoader();
                    q.UseInMemoryStore();
                    q.UseDefaultThreadPool(tp =>
                    {
                        tp.MaxConcurrency = 10;
                    });
                    foreach (var job in jobs)
                    {
                        var jobKey = new JobKey(job.Type.Name);
                        q.AddJob(job.Type, jobKey);
                        q.AddTrigger(c => c
                            .ForJob(jobKey)
                            .StartAt(DateBuilder.EvenSecondDate(DateTimeOffset.UtcNow.AddSeconds(7)))
                            .WithCronSchedule(job.CronExpr)
                        );
                    }
                });
                services.AddQuartzHostedService(options =>
                {
                    // when shutting down we want jobs to complete gracefully
                    options.WaitForJobsToComplete = true;
                });
            }
            foreach (var job in jobs)
            {
                services.AddTransient(job.Type);
            }
            return services;
        }

        private static IEnumerable<JobRegistrationRecord> GetJobs()
        {
            var types = typeof(ServiceRegistration).Assembly.GetTypes();
            var interfaceType = typeof(IJob);
            var jobs = types
            .Where(t => !t.IsAbstract && !t.IsGenericType && interfaceType.IsAssignableFrom(t))
            .Select(j => new JobRegistrationRecord(j, j.GetCustomAttribute<JobRegistrationAttribute>()?.CronExpr))
            .Where(j => !string.IsNullOrEmpty(j.CronExpr));
            return jobs;
        }
    }

    public record JobRegistrationRecord(Type Type, string? CronExpr);
}
