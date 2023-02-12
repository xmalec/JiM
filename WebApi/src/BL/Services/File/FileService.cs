using BL.Models.File;
using DAL.Repositories;
using DAL.Models;
using Extensions.Extensions;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using System.Drawing;
using BL.Constants;

namespace BL.Services.File
{
    public class FileService : IFileService
    {
        private readonly IBaseRepository<DAL.Models.File> fileRepository;
        private readonly IImageProcessorService imageProcessorService;
        private readonly IMemoryCache memoryCache;

        public FileService(IBaseRepository<DAL.Models.File> fileRepository,
            IMemoryCache memoryCache,
            IImageProcessorService imageProcessorService)
        {
            this.fileRepository = fileRepository;
            this.memoryCache = memoryCache;
            this.imageProcessorService = imageProcessorService;
        }

        public Task<FileDto> GetImage(int fileId, int maxSize)
        {
            return memoryCache.GetOrCreateAsync(GetCacheKey(fileId, maxSize),
                entry =>
                {
                    var fileDto = fileRepository.GetById(fileId).Map<FileDto>();
                    var baseDir = AppDomain.CurrentDomain.BaseDirectory;
                    fileDto.Data = imageProcessorService.Resize($"{baseDir}/{fileDto.Path}", maxSize);
                    return Task.FromResult(fileDto);
                });
        }

        public Task SaveImage(byte[] binary, FileType fileType)
        {
            throw new NotImplementedException();
        }

        private string GetCacheKey(int fileId, int maxSize)
        {
            return $"file_{fileId}_{maxSize}";
        }
    }
}
