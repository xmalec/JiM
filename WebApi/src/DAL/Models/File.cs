namespace DAL.Models
{
    public class File : BaseEntity
    {
        public string? Name { get; set; }
        public string FileType { get; set; }
        public string Extension { get; set; }
        public int Size { get; set; }
        public int? Height { get; set; }
        public int? Width { get; set; }
        public string Path { get; set; }
    }
}