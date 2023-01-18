using BL.Models.File;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.File
{
    public interface IFileService : IService
    {
        Task<FileWithDataDto> GetFileWithData(int fileId);
    }
}
