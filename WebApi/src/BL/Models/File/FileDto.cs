using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
