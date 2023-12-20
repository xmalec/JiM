using BL.Models.ScheduleTask;

namespace BL.Services.ScheduleTask
{
    public interface IScheduleTaskService : IBaseCRUDService<DAL.Models.ScheduleTask, ScheduleTaskDto>, IService
    {
        public ScheduleTaskDto? GetByName(string name);
    }
}
