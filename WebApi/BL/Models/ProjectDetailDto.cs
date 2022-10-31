namespace BL.Models
{
    public class ProjectDetailDto
    {
        public int Id { get; set; }
        public string ProjectName { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public string LongDescription { get; set; }
        public IList<ImageDto> Images { get; set; }
    }
}