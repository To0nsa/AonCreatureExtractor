using System.Text.RegularExpressions;

namespace AonCreatureExtractor.methods {
	public static partial class TextProcessor
	{
		[GeneratedRegex(@"[ \t]+", RegexOptions.Compiled)]
    	private static partial Regex MultipleSpacesRegex();
		private static readonly char[] SplitChars = [' ', '\t'];


		public static string FormatText(string rawText)
		{
			if (string.IsNullOrWhiteSpace(rawText))
				return string.Empty;

			string	cleanedText = MultipleSpacesRegex().Replace(rawText, " ").TrimEnd();

			return cleanedText;
		}
		
		public static string GetCreatureName(string text, bool isNpc)
		{
			var	lines = text.Split(['\r', '\n'], StringSplitOptions.RemoveEmptyEntries);
			if (lines.Length == 0)
				return "output";

			if (isNpc)
			{
				string	firstLine = lines[0].Trim();
				var		words = firstLine.Split(SplitChars, StringSplitOptions.RemoveEmptyEntries);
				var		selectedWords = words.TakeWhile(word => !word.Equals("CR", StringComparison.OrdinalIgnoreCase));
				string	result = string.Join(" ", selectedWords);
				return string.IsNullOrWhiteSpace(result) ? "output" : result.ToLowerInvariant();
			}
			else
			{
				var		thirdLine = lines[1].Trim();
				var		words = thirdLine.Split(SplitChars, StringSplitOptions.RemoveEmptyEntries);
				var		selectedWords = words.TakeWhile(word => !word.Equals("CR", StringComparison.OrdinalIgnoreCase));
				string	result = string.Join(" ", selectedWords);
				return string.IsNullOrWhiteSpace(result) ? "output" : result.ToLowerInvariant();
			}
		}

		public static string SanitizeFileName(string fileName)
		{
			foreach (char invalidChar in Path.GetInvalidFileNameChars())
			{
				fileName = fileName.Replace(invalidChar, '_');
			}
			return fileName;
		}
	}
}
