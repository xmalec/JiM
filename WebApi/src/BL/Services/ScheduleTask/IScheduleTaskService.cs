using BL.Models.ScheduleTask;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.ScheduleTask
{
    public interface IScheduleTaskService : IBaseCRUDService<DAL.Models.ScheduleTask, ScheduleTaskDto>, IService
    {
        public ScheduleTaskDto? GetByName(string name);
    }
}
