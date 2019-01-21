using Handler;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using URLShortener.Controllers;

namespace Test
{
    [TestClass]
    public class UnitTest1
    {
		private readonly IUrlShortenerHandler urlShortenerHandler = Substitute.For<IUrlShortenerHandler>();
		private UrlShortenerController controller;
		[TestInitialize]
		public void InitialData()
		{
			this.controller = new UrlShortenerController(this.urlShortenerHandler); 
		}
        [TestMethod]
        public void WhenSendInvalidUrlReturnBadRequestStatus()
        {
			this.urlShortenerHandler.ValidateUrl(Arg.Any<string>()).Returns((false, "Invalid Url"));
			this.urlShortenerHandler.GetShortUrl(Arg.Any<string>()).Returns(" ");

			var result = this.controller.Get("This is not an url");

			this.urlShortenerHandler.Received(1).ValidateUrl(Arg.Any<string>());
			this.urlShortenerHandler.Received(0).GetShortUrl(Arg.Any<string>());
			
			Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
		}

		[TestMethod]
		public void WhenSendValidUrlReturnOKStatus()
		{
			this.urlShortenerHandler.ValidateUrl(Arg.Any<string>()).Returns((true, " "));
			this.urlShortenerHandler.GetShortUrl(Arg.Any<string>()).Returns(" ");

			var result = this.controller.Get("http://itgirlblog.com");

			this.urlShortenerHandler.Received(1).ValidateUrl(Arg.Any<string>());
			this.urlShortenerHandler.Received(1).GetShortUrl(Arg.Any<string>());

			Assert.IsInstanceOfType(result, typeof(OkObjectResult));
		}
    }
}
