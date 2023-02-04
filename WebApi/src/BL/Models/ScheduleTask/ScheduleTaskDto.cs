using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Models.ScheduleTask
{
    public class ScheduleTaskDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsRunning { get; set; }
        public string Description { get; set; }
        public DateTime LastRun { get; set; }
    }
}
