using System.Net.Http;
using System.Threading.Tasks;

/// <summary>
/// Provides functionality to fetch HTML content from a specified URL asynchronously.
/// </summary>
public static class HtmlFetcher
{
	// Reuse a single instance of HttpClient for better performance and resource management.
	private static readonly HttpClient _httpClient = new HttpClient();

	/// <summary>
	/// Asynchronously fetches the HTML content from the specified URL.
	/// </summary>
	/// <param name="url">The URL from which to fetch HTML content.</param>
	/// <returns>
	/// A task that represents the asynchronous operation. The task result contains the HTML content as a string.
	/// </returns>
	public static async Task<string> FetchHtmlAsync(string url)
	{
		// Optionally, you could add validation for the URL here (e.g., null or empty check)
		// and throw an ArgumentException if necessary.

		// Fetch and return the HTML content as a string.
		return await _httpClient.GetStringAsync(url);
	}
}
