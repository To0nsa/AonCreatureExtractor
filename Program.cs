using System;
using System.Linq;
using System.Threading.Tasks;

class Program
{
	static async Task Main(string[] args)
	{
		if (args.Length == 0)
		{
			Console.WriteLine("Usage: dotnet run <URL1> <URL2> ... <URLN>");
			return;
		}

		// Process all URLs concurrently for improved performance.
		var tasks = args.Select(ProcessUrlAsync);
		await Task.WhenAll(tasks);
	}

	private static async Task ProcessUrlAsync(string url)
	{
		try
		{
			Logger.PrintHeader($"Fetching HTML from: {url}");
			string htmlContent = await HtmlFetcher.FetchHtmlAsync(url);
			Logger.PrintHeader($"Extracting and cleaning text: {url}");
			string extractedText = HtmlParser.ExtractText(htmlContent);
			Logger.PrintText(extractedText);
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error processing {url}: {ex.Message}");
		}
	}
}
