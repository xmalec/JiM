namespace DAL.Models
{
    public class Contact : BaseEntity
    {
        public string IPAddress { get; set; }
        public string Location { get; set; }
        public DateTime DateTime { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Content { get; set; }
    }
}