using System;
using Iklian.Data;
using Iklian.Web.Areas.Api.Controllers;
using Iklian.Web.Areas.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using NUnit.Framework;

namespace Iklian.Web.Test.Areas.Api.Controllers
{
    public class ApiControllerTests
    {
        private const string ValidUrl = "https://www.canada.ca";
        private readonly Mock<IUrlAliasData> _urlAliasDataMoq = new Mock<IUrlAliasData>();
        private readonly Mock<IObjectModelValidator> _objectModelValidatorMock = new Mock<IObjectModelValidator>();

        private ApiController _subject;

        
        [SetUp]
        public void Setup()
        {
            _urlAliasDataMoq.Reset();

            _objectModelValidatorMock.Setup(o => o.Validate(It.IsAny<ActionContext>(),
                It.IsAny<ValidationStateDictionary>(),
                It.IsAny<string>(),
                It.IsAny<Object>()));
        }

        [Test]
        public void GenerateAlias_WithValidRequest_ReturnsWithJsonResult()
        {
            _subject = new ApiController(NullLogger<ApiController>.Instance, _urlAliasDataMoq.Object);
            var response = _subject.GenerateAlias(new GenerateAliasRequest {Url = ValidUrl});

            var jsonResult = response as JsonResult;
            Assert.IsNotNull(jsonResult);
            var urlGenerateResponse = jsonResult.Value as GenerateAliasResponse;
            Assert.IsNotNull(urlGenerateResponse);
            Assert.IsNotNull(urlGenerateResponse.Alias);
        }
    }
}