using Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Globalization;

namespace Web.Controllers
{
    [Route("[controller]/[action]")]
    public class TestController : Controller
    {
        private readonly IStringLocalizer<SharedResource> localizedizer;

        public TestController(IStringLocalizer<SharedResource> localizedizer)
        {
            this.localizedizer = localizedizer;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var current = CultureInfo.CurrentCulture;
            CultureInfo.CurrentCulture = new CultureInfo("cs-CZ");
            current = CultureInfo.CurrentCulture;
            var a = localizedizer["Hello"];
            
            return View();
        }
    }
}
