using AonCreatureExtractor.methods;

class Program
{
	static async Task Main(string[] args)
	{
		if (args.Length == 0)
		{
			Console.WriteLine("Usage: dotnet run <URL1> <URL2> ... <URLN>");
			return ;
		}

		Logger.PrintHeader("Hello Faust what's up ? Ready to extract some text ?");

		var	tasks = args.Select(ProcessUrlAsync);
		await Task.WhenAll(tasks);
	}

	private static async Task ProcessUrlAsync(string url)
	{
		try
		{
			string	htmlContent = await HtmlFetcher.FetchHtmlAsync(url);

			var		(extractedText, isNpc) = HtmlParser.ExtractText(htmlContent);
			
			string	formattedText = TextProcessor.FormatText(extractedText);
			
			string	creatureName = TextProcessor.GetCreatureName(extractedText, isNpc);
			string	safeFileName = TextProcessor.SanitizeFileName(creatureName);
			string	outputDirectory = "CreatureTextFiles";

			Directory.CreateDirectory(outputDirectory);
			string	filePath = Path.Combine(outputDirectory, $"{safeFileName}.txt");

			await File.WriteAllTextAsync(filePath, formattedText);
			Console.WriteLine($"Created file: {filePath}");
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error processing {url}: {ex.Message}");
		}
	}
}

