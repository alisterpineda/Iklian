using System;
using System.Linq;
using Iklian.Core;
using Iklian.Data;
using Iklian.Web.Areas.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Iklian.Web.Areas.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UrlController : ControllerBase
    {
        private readonly ILogger<UrlController> _logger;
        private readonly IUrlAliasData _urlAliasData;
        private readonly Random _random = new Random();

        public UrlController(ILogger<UrlController> logger, IUrlAliasData urlAliasData)
        {
            _logger = logger;
            _urlAliasData = urlAliasData;
        }

        [HttpPost]
        public IActionResult Generate([FromBody] UrlGenerateRequest request)
        {
            var creationDate = DateTime.Now;
            var hash = GenerateRandomString(4);

            var shortUrl = new UrlAlias
            {
                Url = request.Url,
                Alias = hash,
                CreationDate = creationDate
            };

            _urlAliasData.Add(shortUrl);
            _urlAliasData.Commit();

            return new JsonResult(new UrlGenerateResponse {Hash = hash});
        }

        public string GenerateRandomString(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[_random.Next(s.Length)]).ToArray());
        }
    }
}