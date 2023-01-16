using DAL.Repositories;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using File = DAL.Models.File;
using BL.Services.File;
using Microsoft.AspNetCore.Authorization;
using AzureSearch.Services.ParentPage;

namespace WebApi.Controllers
{
    public class SearchController : ApiControllerBase
    {
        private readonly IParentPageSearchService parentPageSearchService;

        public SearchController(IParentPageSearchService parentPageSearchService)
        {
            this.parentPageSearchService = parentPageSearchService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> RebuildAsync()
        {
            await parentPageSearchService.RebuildIndex();
            return Ok("OK");
        }
    }
}
