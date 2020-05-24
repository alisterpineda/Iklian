using System;
using Iklian.Core;
using Iklian.Data;
using Iklian.Web.Controllers;
using Iklian.Web.Core.Exceptions;
using Iklian.Web.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using NUnit.Framework;

namespace Iklian.Web.Test.Controllers
{
    class HomeControllerTests
    {
        private const string ValidUrl = "https://www.canada.ca";
        private readonly Mock<IUrlAliasData> _urlAliasDataMoq = new Mock<IUrlAliasData>();
        private readonly Mock<IExceptionHandlerPathFeature> _iExceptionHandlerPathFeature = new Mock<IExceptionHandlerPathFeature>();
        private HomeController _subject;

        [SetUp]
        public void Setup()
        {
            _urlAliasDataMoq.Reset();
            _subject = new HomeController(NullLogger<HomeController>.Instance, _urlAliasDataMoq.Object){ControllerContext = new ControllerContext{HttpContext = new DefaultHttpContext()}};
        }

        [Test]
        public void Index_ReturnsViewResult()
        {
            var response = _subject.Index();

            var viewResult = response as ViewResult;
            Assert.IsNotNull(viewResult);
        }

        [Test]
        public void RedirectFromAlias_WithValidAlias_ReturnsRedirectResult()
        {
            _urlAliasDataMoq.Setup(x => x.GetUrlAliasFromAlias(It.IsAny<string>())).Returns(new UrlAlias{Url = ValidUrl});

            var response = _subject.RedirectFromAlias("abc");

            var redirectResult = response as RedirectResult;
            Assert.IsNotNull(redirectResult);
            Assert.AreEqual(ValidUrl, redirectResult.Url);
        }

        [Test]
        public void RedirectFromAlias_WithInvalidAlias_ThrowsException()
        {
            _urlAliasDataMoq.Setup(x => x.GetUrlAliasFromAlias(It.IsAny<string>())).Returns((UrlAlias)null);

            Assert.Throws<AliasNotFoundException>(() => _subject.RedirectFromAlias("abc"));
        }

        [Test]
        public void Error_WithIklianWebException_ReturnsView()
        {
            _iExceptionHandlerPathFeature.SetupGet(x => x.Error).Returns(new AliasNotFoundException());
            _subject.HttpContext.Features.Set(_iExceptionHandlerPathFeature.Object);

            var response = _subject.Error();
            
            var viewResult = response as ViewResult;
            Assert.IsNotNull(viewResult);
            var model = viewResult.Model as ErrorViewModel;
            Assert.AreEqual(StatusCodes.Status404NotFound, model?.StatusCode);
            Assert.AreEqual("Not Found", model?.StatusCodeDefinition);
        }

        [Test]
        public void Error_WithMiscellaneousException_ReturnsView()
        {
            _iExceptionHandlerPathFeature.SetupGet(x => x.Error).Returns(new Exception());
            _subject.HttpContext.Features.Set(_iExceptionHandlerPathFeature.Object);

            var response = _subject.Error();

            var viewResult = response as ViewResult;
            Assert.IsNotNull(viewResult);
            var model = viewResult.Model as ErrorViewModel;
            Assert.AreEqual(StatusCodes.Status500InternalServerError, model?.StatusCode);
            Assert.AreEqual("Internal Server Error", model?.StatusCodeDefinition);
        }
    }
}
