
namespace Domain
{
    public class UrlModel
    {
		public UrlModel(string id, string url)
		{
			this.Url = url;
			this.Id = id;
		}
		public string Url { set; get; }

		public string Id { set; get; }
    }
}
