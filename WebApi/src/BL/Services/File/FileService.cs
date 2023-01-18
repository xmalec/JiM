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
        private readonly IMapper mapper;
        private readonly IMemoryCache memoryCache;

        public FileService(IBaseRepository<DAL.Models.File> fileRepository,
            IMapper mapper,
            IMemoryCache memoryCache)
        {
            this.fileRepository = fileRepository;
            this.mapper = mapper;
            this.memoryCache = memoryCache;
        }

        public Task<FileWithDataDto> GetFileWithData(int fileId)
        {
            return memoryCache.GetOrCreateAsync(GetCacheKey(fileId),
                entry =>
                {
                    return Task.FromResult(fileRepository.Find(fileId).Map<FileWithDataDto>(mapper));
                });
        }

        private string GetCacheKey(int fileId)
        {
            return $"file_{fileId}";
        }
    }
}
