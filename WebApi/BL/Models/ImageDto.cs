using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Models
{
    public class ImageDto
    {
        public string FilePath { get; set; }
        public string? Name { get; set; }
        public string? Variant { get; set; }
        public string? Alt { get; set; }
    }
}
