using DAL.Repositories;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using File = DAL.Models.File;
using BL.Services.File;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controllers
{
    public class SearchController : ApiControllerBase
    {
        private readonly IFileService fileService;

        public SearchController(IFileService fileService)
        {
            this.fileService = fileService;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Get(int id)
        {
            var file = fileService.GetFileWithData(id);       
            return File(file.Data, file.FileType);
        }
    }
}
