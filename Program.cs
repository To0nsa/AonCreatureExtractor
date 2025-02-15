using System;
using System.Linq;
using System.IO;
using System.Threading.Tasks;

class Program
{
	// Entry point: Asynchronously processes URLs passed via command-line arguments.
	static async Task Main(string[] args)
	{
		// Ensure at least one URL is provided.
		if (args.Length == 0)
		{
			Console.WriteLine("Usage: dotnet run <URL1> <URL2> ... <URLN>");
			return;
		}

		Logger.PrintHeader("Hello Faust what's up ? Ready to extract some text ?");

		// Process all URLs concurrently for improved performance.
		// Each URL will be processed by ProcessUrlAsync.
		var tasks = args.Select(url => ProcessUrlAsync(url));
		await Task.WhenAll(tasks);
	}

	// Processes an individual URL:
	// 1. Fetches HTML content.
	// 2. Extracts raw text.
	// 3. Formats the text.
	// 4. Extracts a creature name for the file name.
	// 5. Sanitizes the file name.
	// 6. Writes the formatted text to a .txt file.
	private static async Task ProcessUrlAsync(string url)
	{
		try
		{
			// Fetch HTML content from the URL.
			string htmlContent = await HtmlFetcher.FetchHtmlAsync(url);

			// Extract raw text from the fetched HTML.
			var (extractedText, isNpc) = HtmlParser.ExtractText(htmlContent);

			// Format the extracted text
			string formattedText = TextProcessor.FormatText(extractedText);

			// Extract the creature name from the raw text.
			string creatureName = TextProcessor.GetCreatureName(extractedText, isNpc);

			// Sanitize the creature name to ensure it's a valid file name.
			string safeFileName = TextProcessor.SanitizeFileName(creatureName);

			// Specify the output directory (change this path as needed).
			string outputDirectory = "CreatureTextFiles";

			// Create the output directory if it doesn't exist.
			Directory.CreateDirectory(outputDirectory);

			// Combine the directory and file name to create the full path.
			string filePath = Path.Combine(outputDirectory, $"{safeFileName}.txt");

			// Write the formatted text to the .txt file.
			await File.WriteAllTextAsync(filePath, formattedText);

			Console.WriteLine($"Created file: {filePath}");
		}
		catch (Exception ex)
		{
			// Log errors with the URL that caused the problem.
			Console.WriteLine($"Error processing {url}: {ex.Message}");
		}
	}
}

