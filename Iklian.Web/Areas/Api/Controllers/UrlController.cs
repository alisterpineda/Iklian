using System;
using System.Text;
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

        public UrlController(ILogger<UrlController> logger, IUrlAliasData urlAliasData)
        {
            _logger = logger;
            _urlAliasData = urlAliasData;
        }

        [HttpPost]
        public IActionResult Generate([FromBody] UrlGenerateRequest request)
        {
            var urlAlias = new UrlAlias();
            _urlAliasData.Add(urlAlias);
            _urlAliasData.Commit();

            urlAlias.Url = request.Url;
            urlAlias.Alias = GenerateRandomString(urlAlias.Id);
            urlAlias.CreationDate = DateTime.Now;
            _urlAliasData.Update(urlAlias);
            _urlAliasData.Commit();

            return new JsonResult(new UrlGenerateResponse {Alias = urlAlias.Alias});
        }

        private string EncodeInt32AsString(int input, int maxLength = 0)
        {
            var allowedList = new[] {
                '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
                'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j',
                'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't',
                'u', 'v', 'w', 'x', 'y', 'z',
                'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J',
                'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T',
                'U', 'V', 'W', 'X', 'Y', 'Z' };
            var allowedSize = allowedList.Length;
            var result = new StringBuilder(input.ToString().Length);

            while (input > 0)
            {
                var moduloResult = input % allowedSize;
                input /= allowedSize;
                result.Insert(0, allowedList[moduloResult]);
            }

            if (maxLength > result.Length)
            {
                result.Insert(0, new string(allowedList[0], maxLength - result.Length));
            }

            if (maxLength > 0)
                return result.ToString().Substring(0, maxLength);
            return result.ToString();
        }

        private string GenerateRandomString(int input, int uniqueLength = 4, int randomLength = 4)
        {
            var resultString = new StringBuilder(uniqueLength + randomLength);

            var random = new Random();

            var randomString = EncodeInt32AsString(random.Next(1, int.MaxValue), randomLength);
            var uniqueString = EncodeInt32AsString(input, uniqueLength);

            // Interleave the two strings
            for (var i = 0; i < Math.Min(uniqueLength, randomLength); i++)
            {
                resultString.AppendFormat("{0}{1}", uniqueString[i], randomString[i]);
            }
            resultString.Append((uniqueLength < randomLength ? randomString : uniqueString).Substring(Math.Min(uniqueLength, randomLength)));

            return resultString.ToString();
        }
    }
}