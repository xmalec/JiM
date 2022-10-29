namespace DAL.Models
{
    public class Image : BaseEntity
    {
        public string FilePath { get; set; }
        public string? Name { get; set; }
        public string? Variant { get; set; }
        public string FileTypeExtension { get; set; }
        public string? Alt { get; set; }
        public string? Key { get; set; }
        public bool? IsMain { get; set; }
        public Project? Project { get; set; }
    }
}