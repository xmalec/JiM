using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Options
{
    public class FileSettingOptions
    {
        public const string SectionName = "FileSetting";

        public string RootFolderName { get; set; }
        public int DefaultCompressWidth { get; set; }
    }
}
