using BL.Models.ScheduleTask;
using BL.Services.ScheduleTask;
using Microsoft.Extensions.Logging;
using Quartz;

namespace ScheduleTasks.Jobs
{
    public abstract class DbJob : IJob
    {
        private readonly IScheduleTaskService scheduleTaskService;
        private readonly ILogger logger;

        internal abstract string JobName { get; }

        protected DbJob(IScheduleTaskService scheduleTaskService,
            ILogger logger)
        {
            this.scheduleTaskService = scheduleTaskService;
            this.logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            if(await BeforeRun())
            {
                logger.LogInformation($"Schedule task {JobName} starting.");
                var message = "OK";
                try
                {
                    await RunJob();
                } catch(Exception ex)
                {
                    message = ex.Message;
                    logger.LogError($"Error occured while proccessing schedule task {JobName}: {ex.Message}" +
                        $"{Environment.NewLine}" +
                        $"Stack trace: {ex.StackTrace}");
                }
                await AfterRun(message);
            }
        }

        private async Task<bool> BeforeRun()
        {
            var st = scheduleTaskService.GetByName(JobName);
            if (st == null)
            {
                var id = await scheduleTaskService.Add(new ScheduleTaskDto()
                {
                    Name = JobName
                });
                logger.LogInformation($"Schedule task {JobName} created.");
                st = scheduleTaskService.GetById(id);
            }
            if(st.IsRunning)
            {
                logger.LogInformation($"Schedule task {JobName} already running.");
                return false;
            }
            st.IsRunning = true;
            await scheduleTaskService.Update(st);
            return true;
        }

        private async Task AfterRun(string message)
        {
            var st = scheduleTaskService.GetByName(JobName);
            if (st == null)
            {
                logger.LogError($"Schedule task {JobName} not found in db after run!");
                return;
            }
            st.IsRunning = false;
            st.Description= message;
            st.LastRun = DateTime.Now;
            await scheduleTaskService.Update(st);
        }

        internal abstract Task RunJob();
    }
}
