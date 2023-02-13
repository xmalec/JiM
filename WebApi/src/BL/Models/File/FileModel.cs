using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Models.File
{
    public class FileModel
    {
        public byte[]? Binary { get; set; }
        public string Name { get; set; }
        public string FileType { get; set; }
    }
}
