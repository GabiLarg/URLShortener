using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public interface IUrlShortenerRepository
    {
		string GetUrl(string id);
		void SetShortUrl(string id, string url);
	}


}
