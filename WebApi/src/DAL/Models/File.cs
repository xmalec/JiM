using DAL.Enums;

namespace DAL.Models
{
    public class File : BaseEntity
    {
        public string FilePath { get; set; }
        public string? Name { get; set; }
        public FileType FileType { get; set; }
        public string Extension { get; set; }
        public int Size { get; set; }
        public DateTime DateCreated { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
    }
}