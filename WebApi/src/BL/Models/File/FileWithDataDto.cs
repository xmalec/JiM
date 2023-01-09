using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Models.File
{
    public class FileWithDataDto
    {
        public int Id { get; set; }
        public byte[] Data { get; set; }
        public string FileType { get; set; }
    }
}
