using System.Diagnostics;
using Iklian.Data;
using Iklian.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Iklian.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUrlAliasData _urlAliasData;

        public HomeController(ILogger<HomeController> logger, IUrlAliasData urlAliasData)
        {
            _logger = logger;
            _urlAliasData = urlAliasData;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("{alias}")]
        public IActionResult RedirectFromAlias(string alias)
        {
            var urlAlias = _urlAliasData.GetUrlAliasFromAlias(alias);
            if (urlAlias == null)
            {
                return NotFound();
            }
            return Redirect(urlAlias.Url);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error([FromQuery] int code)
        {

            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                StatusCode = code
            });
        }
    }
}
