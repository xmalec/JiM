using Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.Localization;
using System.Globalization;

namespace Web.Controllers
{
    [Route("[controller]/[action]")]
    public class TestController : Controller
    {
        private readonly IStringLocalizer<SharedResource> localizer;
        private readonly IHtmlLocalizer<SharedResource> htmlLocalizer;

        public TestController(IStringLocalizer<SharedResource> localizer, IHtmlLocalizer<SharedResource> htmlLocalizer)
        {
            this.localizer = localizer;
            this.htmlLocalizer = htmlLocalizer;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var hello = localizer["Hello"];
            return View();
        }
    }
}
