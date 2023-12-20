using BL.Models.ScheduleTask;
using DAL.Repositories;
using Extensions.Extensions;

namespace BL.Services.ScheduleTask
{
    public class ScheduleTaskService : BaseCRUDService<DAL.Models.ScheduleTask, ScheduleTaskDto>, IScheduleTaskService
    {
        public ScheduleTaskService(IBaseRepository<DAL.Models.ScheduleTask> repository) : base(repository)
        {
        }

        public ScheduleTaskDto? GetByName(string name)
        {
            return repository
                .Query()
                .FirstOrDefault(x => x.Name == name)
                ?.Map<ScheduleTaskDto>();
        }
    }
}
