namespace DAL.Models
{
    public class ProjectLog : BaseEntity
    {
        public string IPAddress { get; set; }
        public string Location { get; set; }
        public DateTime DateTime { get; set; }
        public Project Project { get; set; }
    }
}