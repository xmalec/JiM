using System.Security.Policy;

namespace DAL.Models
{
    public class EventLog : BaseEntity
    {
        public string? Source { get; set; }
        public string Level { get; set; }
        public string? Description { get; set; }
        public string? Exception { get; set; }
        public string Event { get; set; }
        public string? CallStack { get; set; }
    }
}