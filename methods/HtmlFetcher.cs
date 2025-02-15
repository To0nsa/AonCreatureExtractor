namespace AonCreatureExtractor.methods {
	public static class HtmlFetcher
	{
		private static readonly HttpClient _httpClient = new();

		public static async Task<string> FetchHtmlAsync(string url)
		{
			return await _httpClient.GetStringAsync(url);
		}
	}
}
