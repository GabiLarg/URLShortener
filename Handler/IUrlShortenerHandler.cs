
namespace Handler
{
    public interface IUrlShortenerHandler
    {
		(bool, string) ValidateUrl(string url);

		string GetShortUrl(string url);

		(bool, string) GetUrl(string id);
	}
}
