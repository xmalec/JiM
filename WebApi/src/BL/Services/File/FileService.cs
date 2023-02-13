using BL.Models.File;
using DAL.Repositories;
using DAL.Models;
using Extensions.Extensions;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using System.Drawing;
using BL.Constants;
using Microsoft.Extensions.Logging;
using BL.Options;
using Microsoft.Extensions.Options;

namespace BL.Services.File
{
    public class FileService : IFileService
    {
        private readonly IBaseRepository<DAL.Models.File> fileRepository;
        private readonly IImageProcessorService imageProcessorService;
        private readonly IMemoryCache memoryCache;
        private readonly ILogger<FileService> logger;
        private readonly FileSettingOptions fileSetting;

        public FileService(IBaseRepository<DAL.Models.File> fileRepository,
            IMemoryCache memoryCache,
            IImageProcessorService imageProcessorService,
            ILogger<FileService> logger,
            IOptions<FileSettingOptions> fileSettingOptions)
        {
            this.fileRepository = fileRepository;
            this.memoryCache = memoryCache;
            this.imageProcessorService = imageProcessorService;
            this.logger = logger;
            fileSetting = fileSettingOptions.Value;
        }

        public Task<FileDto> GetImage(int fileId, int maxSize)
        {
            return memoryCache.GetOrCreateAsync(GetCacheKey(fileId, maxSize),
                entry =>
                {
                    var fileDto = fileRepository.GetById(fileId).Map<FileDto>();
                    string baseDir = GetBaseDir();
                    fileDto.Data = imageProcessorService.Resize($"{baseDir}/{fileDto.Path}", maxSize).Binary;
                    return Task.FromResult(fileDto);
                });
        }

        private static string GetBaseDir()
        {
            return AppDomain.CurrentDomain.BaseDirectory;
        }

        public async Task SaveFile(FileModel fileModel, FileType fileType)
        {
            var fileName = $"{Guid.NewGuid().ToString()[..5]}_{fileModel.Name}";
            var path = $"{fileSetting.RootFolderName}/{fileType.FolderName}/{fileName}";
            var file = new DAL.Models.File
            {
                Name = fileModel.Name,
                Extension = fileModel.Name.Substring(fileModel.Name.LastIndexOf('.')),
                FileType = fileModel.FileType,
                Path = path
            };
            if ((new FileType[] {FileType.ImageUserPage, FileType.ImagePost}).Contains(fileType))
            {
                var compressResult = imageProcessorService.Compress(fileModel.Binary, fileSetting.DefaultCompressWidth);
                fileModel.Binary = compressResult.Binary;
                file.Height = compressResult.Height;
                file.Width = compressResult.Width;
            }
            SaveFile(fileModel.Binary, path);
            file.Size = fileModel.Binary.Length;
            await fileRepository.Insert(file);
        }

        private void SaveFile(byte[] binary, string path)
        {
            string baseDir = GetBaseDir();
            string filePath = $"{baseDir}/{path}";
            try
            {
                System.IO.File.WriteAllBytes(filePath, binary);
            } catch(Exception ex)
            {
                logger.LogError($"Error during saving file on path {filePath}{Environment.NewLine}" +
                    $"Exception: {ex.Message}{Environment.NewLine}" +
                    $"Stack Trace: {ex.StackTrace}");
                throw ex;
            }
            
        }

        private string GetCacheKey(int fileId, int maxSize)
        {
            return $"file_{fileId}_{maxSize}";
        }
    }
}
