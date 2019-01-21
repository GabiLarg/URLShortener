using Domain;
using Microsoft.Extensions.Caching.Memory;
using System;

namespace Repository
{
	public class UrlShortenerRepository : IUrlShortenerRepository
	{
		private readonly IMemoryCache cache;
			
		public UrlShortenerRepository(IMemoryCache cache)
		{
			this.cache = cache ?? throw new ArgumentNullException(nameof(cache));
		}

		public string GetUrl(string id)
		{
			if (this.cache.TryGetValue<UrlModel>(id, out var url))
			{
				return url.Url;
			}
			else
			{
				throw new Exception();
			}
			
		}

		public void SetShortUrl(string id, string url)
		{
			this.cache.Set<UrlModel>(id, new UrlModel(id, url));
		}
	}
}
