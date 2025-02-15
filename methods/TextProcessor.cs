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
	public static string GetCreatureName(string text)
	{
		// Split the text into lines and select the first non-empty line.
		var firstLine = text
			.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
			.FirstOrDefault();

		// If no valid line is found, return "output"; otherwise, trim and convert to lowercase.
		return string.IsNullOrWhiteSpace(firstLine)
			? "output"
			: firstLine.Trim().ToLowerInvariant();
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
