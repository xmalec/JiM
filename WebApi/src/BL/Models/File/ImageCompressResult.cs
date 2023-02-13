using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Models.File
{
    public class ImageCompressResult
    {
        public byte[] Binary { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
    }
}
