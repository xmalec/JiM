namespace BL.Dtos
{
    public class ProjectPreviewItem
    {
        public int Id { get; set; }
        public string ProjectName { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public ImageDto Image { get; set; }
    }
}