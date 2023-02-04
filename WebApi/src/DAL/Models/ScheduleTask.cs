namespace DAL.Models
{
    public class ScheduleTask : BaseEntity
    {
        public string Name { get; set; }
        public bool IsRunning { get; set; }
        public string? Description { get; set; }
        public DateTime? LastRun { get; set; }
    }
}