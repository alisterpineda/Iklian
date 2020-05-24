using System.Diagnostics;
using Iklian.Data;
using Iklian.Web.Core.Exceptions;
using Iklian.Web.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
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
                throw new AliasNotFoundException();
            }
            return Redirect(urlAlias.Url);
        }

        [Route("/Error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var exceptionHandlerPathFeature =
                HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            var exception = exceptionHandlerPathFeature?.Error as IklianWebException;
            var statusCode = StatusCodes.Status500InternalServerError;

            if (exception != null)
            {
                statusCode = exception.StatusCode;
            }

            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                StatusCode = statusCode,
                StatusCodeDefinition = ReasonPhrases.GetReasonPhrase(statusCode)
            });
        }
    }
}
