using DAL.Repositories;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using File = DAL.Models.File;
using BL.Services.File;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controllers
{
    public class FileController : ApiControllerBase
    {
        private readonly IFileService fileService;

        public FileController(IFileService fileService)
        {
            this.fileService = fileService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAsync(int id, int maxSize = 10000)
        {
            var file = await fileService.GetImage(id, maxSize);       
            return File(file.Data, file.FileType);
        }
    }
}
