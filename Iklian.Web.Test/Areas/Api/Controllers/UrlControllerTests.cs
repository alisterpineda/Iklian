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
    public class UrlControllerTests
    {
        private const string ValidUrl = "https://www.canada.ca";
        private const string InvalidUrl = "abc";
        private readonly Mock<IUrlAliasData> _urlAliasDataMoq = new Mock<IUrlAliasData>();
        private readonly Mock<IObjectModelValidator> _objectModelValidatorMock = new Mock<IObjectModelValidator>();

        private UrlController _subject;

        
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
        public void Generate_WithValidRequest_ReturnsWithJsonResult()
        {
            _subject = new UrlController(NullLogger<UrlController>.Instance, _urlAliasDataMoq.Object){ ObjectValidator = _objectModelValidatorMock.Object};
            var response = _subject.Generate(new UrlGenerateRequest {Url = ValidUrl});

            var jsonResult = response as JsonResult;
            Assert.IsNotNull(jsonResult);
            var urlGenerateResponse = jsonResult.Value as UrlGenerateResponse;
            Assert.IsNotNull(urlGenerateResponse);
            Assert.IsNotNull(urlGenerateResponse.Alias);
        }

        [Test]
        public void Generate_WithInvalidRequest_ReturnsWithUnprocessableEntityErrorCode()
        {
            _subject = new UrlController(NullLogger<UrlController>.Instance, _urlAliasDataMoq.Object) { ObjectValidator = _objectModelValidatorMock.Object};
            _subject.ModelState.AddModelError("Url", "The Url is not a valid URL"); // force validation to fail

            var response = _subject.Generate(new UrlGenerateRequest { Url = InvalidUrl });

            var jsonResult = response as UnprocessableEntityResult;
            Assert.IsNotNull(jsonResult);
        }
    }
}