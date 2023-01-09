using BL.Models.File;
using DAL.Repositories;
using DAL.Models;
using Extensions.Extensions;
using AutoMapper;

namespace BL.Services.File
{
    public class FileService : IFileService
    {
        private readonly IBaseRepository<DAL.Models.File> fileRepository;
        private readonly IMapper mapper;

        public FileService(IBaseRepository<DAL.Models.File> fileRepository, IMapper mapper)
        {
            this.fileRepository = fileRepository;
            this.mapper = mapper;
        }

        public FileWithDataDto GetFileWithData(int fileId)
        {
            return fileRepository.Find(fileId).Map<FileWithDataDto>(mapper);
        }
    }
}
