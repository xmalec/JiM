using BL.Models.ScheduleTask;
using DAL.Repositories;
using Extensions.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
