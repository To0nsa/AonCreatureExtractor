using System;

/// <summary>
/// Provides simple logging functionality for printing headers and text to the console.
/// </summary>
public static class Logger
{
	/// <summary>
	/// Prints a formatted header with a title to the console.
	/// A header is printed with a blank line before it and lines of dashes above and below the title.
	/// </summary>
	/// <param name="title">The header title to print.</param>
	public static void PrintHeader(string title)
	{
		// Print a new line followed by a line of 40 dashes.
		Console.WriteLine(Environment.NewLine + new string('-', 40));
		
		// Print the header title.
		Console.WriteLine(title);
		
		// Print another line of 40 dashes to complete the header.
		Console.WriteLine(new string('-', 40));
	}

	/// <summary>
	/// Prints text to the console, optionally truncating it if it exceeds a specified maximum length.
	/// If the text is longer than the specified length, it is truncated and appended with an ellipsis ("...").
	/// </summary>
	/// <param name="text">The text to print.</param>
	/// <param name="length">
	/// Optional maximum length for the displayed text. 
	/// If provided and the text exceeds this length, the text is truncated.
	/// </param>
	public static void PrintText(string text, int? length = null)
	{
		// Check if a maximum length is provided and the text is longer than the limit.
		if (length.HasValue && text.Length > length.Value)
		{
			// Print the text truncated to the specified length, followed by an ellipsis.
			Console.WriteLine(text.Substring(0, length.Value) + "...");
		}
		else
		{
			// Print the full text.
			Console.WriteLine(text);
		}
	}
}

