using Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Globalization;

namespace Web.Controllers
{
    [Route("[controller]/[action]")]
    public class TestController : Controller
    {
        private readonly IStringLocalizer<SharedResource> localizer;

        public TestController(IStringLocalizer<SharedResource> localizer)
        {
            this.localizer = localizer;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var hello = localizer["Hello"];
            var brandName = localizer[SharedResource.BrandName];
            return View();
        }
    }
}
