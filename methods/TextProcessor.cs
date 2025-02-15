using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

/// <summary>
/// Provides utility methods for processing text, including formatting,
/// extracting a creature name from text, and sanitizing file names.
/// </summary>
public static class TextProcessor
{
	/// <summary>
	/// Formats the provided raw text by reducing multiple spaces or tabs to a single space.
	/// </summary>
	/// <param name="rawText">The raw text to format.</param>
	/// <returns>A formatted version of the text with reduced whitespace.</returns>
	public static string FormatText(string rawText)
	{
		// Return an empty string if input is null, empty, or whitespace.
		if (string.IsNullOrWhiteSpace(rawText))
			return string.Empty;

		// Use Regex to replace multiple spaces or tabs with a single space.
		string cleanedText = Regex.Replace(rawText, @"[ \t]+", " ", RegexOptions.Compiled);

		return cleanedText;
	}
	
	/// <summary>
	/// Extracts the creature name from the provided text.
	/// It assumes the creature name is the first non-empty line of the text.
	/// The returned name is trimmed and converted to lowercase.
	/// </summary>
	/// <param name="text">The text containing the creature name.</param>
	/// <returns>
	/// The creature name in lowercase and trimmed, or "output" if no valid name is found.
	/// </returns>
	public static string GetCreatureName(string text, bool isNpc)
	{
		// Split the text into non-empty lines.
		var lines = text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
		if (lines.Length == 0)
			return "output";

		if (isNpc)
		{
			// For NPC content, use the first line.
			string firstLine = lines[0].Trim();
			// Split the first line into words.
			var words = firstLine.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
			// Take words until "CR" is encountered.
			var selectedWords = words.TakeWhile(word => !word.Equals("CR", StringComparison.OrdinalIgnoreCase));
			// Join the words with a space.
			string result = string.Join(" ", selectedWords);
			return string.IsNullOrWhiteSpace(result) ? "output" : result.ToLowerInvariant();
		}
		else
		{
			var thirdLine = lines[1].Trim();
			var words = thirdLine.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
			var selectedWords = words.TakeWhile(word => !word.Equals("CR", StringComparison.OrdinalIgnoreCase));
			string result = string.Join(" ", selectedWords);
			return string.IsNullOrWhiteSpace(result) ? "output" : result.ToLowerInvariant();
		}
	}

	/// <summary>
	/// Sanitizes a file name by replacing any characters that are invalid in file names with an underscore.
	/// </summary>
	/// <param name="fileName">The file name to sanitize.</param>
	/// <returns>A sanitized file name safe for use in file systems.</returns>
	public static string SanitizeFileName(string fileName)
	{
		// Iterate through invalid file name characters and replace them with an underscore.
		foreach (char invalidChar in Path.GetInvalidFileNameChars())
		{
			fileName = fileName.Replace(invalidChar, '_');
		}
		return fileName;
	}
}
