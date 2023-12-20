using BL.Constants;
using BL.Models.File;

namespace BL.Services.File
{
    public interface IFileService : IService
    {
        Task<FileDto> GetImage(int fileId, int maxSize);
        Task SaveFile(FileModel fileModel, FileType fileType);
    }
}
