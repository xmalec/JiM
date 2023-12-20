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
