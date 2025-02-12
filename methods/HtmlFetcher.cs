using System.Net.Http;
using System.Threading.Tasks;

public static class HtmlFetcher
{
	private static readonly HttpClient _httpClient = new HttpClient();

	public static async Task<string> FetchHtmlAsync(string url)
	{
		return await _httpClient.GetStringAsync(url);
	}
}

