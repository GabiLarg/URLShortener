using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Handler;
using Microsoft.Extensions.Configuration;

namespace URLShortener.Controllers
{
    [Route("/")]
    public class UrlShortenerController : Controller
    {
		private readonly IUrlShortenerHandler urlShortenerHandler;
		

		public UrlShortenerController(IUrlShortenerHandler urlShortenerHandler)
		{
			this.urlShortenerHandler = urlShortenerHandler ?? throw new ArgumentNullException(nameof(urlShortenerHandler));
			
		}

		[HttpGet("{url}")]
		[Route("short")]
        public IActionResult Get(	string url)
        {
			var (isValid, response) = this.urlShortenerHandler.ValidateUrl(url);
			if (!isValid) { return BadRequest(response); }

			var link = this.urlShortenerHandler.GetShortUrl(url);

            return Ok(link);
        }

		[HttpGet]
		[Route("{id}")]
		public IActionResult RedirectToLink(string id)
		{
			var (isValid, response) = this.urlShortenerHandler.GetUrl(id);
			if (!isValid) { return BadRequest(response); }

			return Redirect(response);
		}

		[HttpGet]
		public IActionResult HealthCheck()
		{
			return Ok("UP");
		}
	}
}	
