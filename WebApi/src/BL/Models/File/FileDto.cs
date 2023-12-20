namespace BL.Models.File
{
    public class FileDto
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public string FileType { get; set; }
        public byte[] Data { get; set; }
    }
}
