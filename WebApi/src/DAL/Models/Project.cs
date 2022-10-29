namespace DAL.Models
{
    public class Project : BaseEntity
    {
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public string LongDescription { get; set; }

    }
}