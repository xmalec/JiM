using System.Security.Policy;

namespace DAL.Models
{
    public class EventLog : BaseEntity
    {
        public string? Source { get; set; }
        public string Level { get; set; }
        public string Description { get; set; }
        public string Exception { get; set; }
        public int EventId { get; set; }
        public int CallStack { get; set; }
    }
}