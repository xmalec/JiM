using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Constants
{
    public class FileType
    {
        public string FolderName { get; set; }

        private FileType(string folderName)
        {
            FolderName = folderName;
        }

        public static FileType ImagePost = new FileType("posts");
        public static FileType ImageUserPage = new FileType("SubFolder");

        public override bool Equals(object? obj)
        {
            return obj is FileType type &&
                   FolderName == type.FolderName;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(FolderName);
        }
    }
}
