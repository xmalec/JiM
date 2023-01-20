using BL.Models.File;
using DAL.Repositories;
using DAL.Models;
using Extensions.Extensions;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;

namespace BL.Services.File
{
    public class FileService : IFileService
    {
        private readonly IBaseRepository<DAL.Models.File> fileRepository;
        private readonly IImageProcessorService imageProcessorService;
        private readonly IMapper mapper;
        private readonly IMemoryCache memoryCache;

        public FileService(IBaseRepository<DAL.Models.File> fileRepository,
            IMapper mapper,
            IMemoryCache memoryCache,
            IImageProcessorService imageProcessorService)
        {
            this.fileRepository = fileRepository;
            this.mapper = mapper;
            this.memoryCache = memoryCache;
            this.imageProcessorService = imageProcessorService;
        }

        public Task<FileWithDataDto> GetFileWithData(int fileId, int maxSize)
        {
            return memoryCache.GetOrCreateAsync(GetCacheKey(fileId, maxSize),
                entry =>
                {
                    var file = fileRepository.GetById(fileId).Map<FileWithDataDto>();
                    file.Data = imageProcessorService.Resize(file.Data, maxSize);
                    return Task.FromResult(file);
                });
        }

        private string GetCacheKey(int fileId, int maxSize)
        {
            return $"file_{fileId}_{maxSize}";
        }
    }
}
