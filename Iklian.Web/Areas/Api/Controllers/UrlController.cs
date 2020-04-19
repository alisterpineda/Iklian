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
        private readonly IShortUrlData _shortUrlData;
        private readonly Random _random = new Random();

        public UrlController(ILogger<UrlController> logger, IShortUrlData shortUrlData)
        {
            _logger = logger;
            _shortUrlData = shortUrlData;
        }

        [HttpPost]
        public IActionResult Generate([FromBody] UrlGenerateRequest request)
        {
            var creationDate = DateTime.Now;
            var hash = GenerateRandomString(4);

            var shortUrl = new ShortUrl
            {
                Url = request.Url,
                Hash = hash,
                CreationDate = creationDate
            };

            _shortUrlData.Add(shortUrl);
            _shortUrlData.Commit();

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