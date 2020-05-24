using Iklian.Core;
using Iklian.Data;
using Iklian.Web.Areas.Api.Controllers;
using Iklian.Web.Areas.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using NUnit.Framework;

namespace Iklian.Web.Test.Areas.Api.Controllers
{
    public class ApiControllerTests
    {
        private const string ValidUrl = "https://www.canada.ca";
        private readonly Mock<IUrlAliasData> _urlAliasDataMoq = new Mock<IUrlAliasData>();

        private ApiController _subject;

        
        [SetUp]
        public void Setup()
        {
            _urlAliasDataMoq.Reset();
            _subject = new ApiController(NullLogger<ApiController>.Instance, _urlAliasDataMoq.Object);
        }

        [Test]
        public void GenerateAlias_WithValidRequest_ReturnsJsonResult()
        {
            var response = _subject.GenerateAlias(new GenerateAliasRequest {Url = ValidUrl});

            var jsonResult = response as JsonResult;
            Assert.IsNotNull(jsonResult);
            var urlGenerateResponse = jsonResult.Value as GenerateAliasResponse;
            Assert.IsNotNull(urlGenerateResponse);
            Assert.IsNotNull(urlGenerateResponse.Alias);
        }

        [Test]
        public void GetUrl_WithValidRequest_ReturnsJsonResult()
        {
            _urlAliasDataMoq.Setup(x => x.GetUrlAliasFromAlias(It.IsAny<string>())).Returns(new UrlAlias{Url = ValidUrl});

            var response = _subject.GetUrl("abc");

            var jsonResult = response as JsonResult;
            Assert.IsNotNull(jsonResult);
            var urlGenerateResponse = jsonResult.Value as GetUrlResponse;
            Assert.IsNotNull(urlGenerateResponse);
            Assert.IsNotNull(urlGenerateResponse.Url);
        }
    }
}