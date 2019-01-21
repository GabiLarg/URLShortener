using Microsoft.Extensions.Configuration;
using Repository;
using System;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace Handler
{
    public class UrlShortenerHandler: IUrlShortenerHandler
    {
		private readonly IUrlShortenerRepository urlShortenerRepository;
		private readonly IConfiguration configuration;
		
		public UrlShortenerHandler(IUrlShortenerRepository urlShortenerRepository, IConfiguration configuration)
		{
			this.urlShortenerRepository = urlShortenerRepository ?? throw new ArgumentNullException(nameof(urlShortenerRepository));
			this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
		}

		public (bool, string) ValidateUrl(string url)
		{

			try
			{
				Uri urlcheck = new Uri(url);

				HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlcheck);
				request.Timeout = 30000;
				HttpWebResponse response;
				response = (HttpWebResponse)request.GetResponse();
				return (true, " ");
			}
			catch (Exception)
			{
				return (false, "Invalid Url");
			}

		}

		public string GetShortUrl(string url)
		{
			var host = this.configuration.GetSection("Address").Value;
			var id = CreateShortUrl(url);

			return $"{host}{id}";
			
		}

		private string CreateShortUrl(string url)
		{
			var md5 = MD5.Create();
			var bytes = Encoding.ASCII.GetBytes(url);

			var hash = md5.ComputeHash(bytes);

			var id = Convert.ToBase64String(hash).Trim('=').Replace("/", "");

			this.urlShortenerRepository.SetShortUrl(id, url);
			return id;
		}

		public (bool, string) GetUrl(string id)
		{
			try
			{
				return (true, this.urlShortenerRepository.GetUrl(id));
			}
			catch (Exception ex)
			{
				return (false, "Invalid Id");
			}
		}

    }
}
