using System;
using System.Text.RegularExpressions;

public static class TextProcessor
{
    public static string FormatText(string rawText)
    {
        if (string.IsNullOrWhiteSpace(rawText))
            return string.Empty;

        string cleanedText = rawText;

        // 1. Reduce multiple spaces to a single space.
        cleanedText = Regex.Replace(cleanedText, @"[ \t]+", " ", RegexOptions.Compiled);

        // 2. Remove colons from section headers and key-value pairs (e.g., "AC: 15" → "AC 15")
        cleanedText = Regex.Replace(cleanedText, @"(?<=\b[A-Z][a-zA-Z]*)\s*:", "", RegexOptions.Compiled);

        // 3. Remove newlines around commas in one go:
        //    - (?<=,) matches a position after a comma.
        //    - (?=,) matches a position before a comma.
        cleanedText = Regex.Replace(cleanedText, @"(?<=,)\s*\r?\n|\r?\n\s*(?=,)", " ", RegexOptions.Compiled);

        // 4. Merge single-letter lines (like "D") with the previous line.
        cleanedText = Regex.Replace(cleanedText, @"\r?\n([A-Z])\b", " $1", RegexOptions.Compiled);

        // 5. Specific formatting for known patterns:
        cleanedText = Regex.Replace(cleanedText, @"(1st— [^\n]+),\s*\r?\n\s*(\(DC \d+\))", "$1 $2", RegexOptions.Compiled);
        cleanedText = Regex.Replace(cleanedText, @"(Init) (\+?\d+);\s*\r?\n\s*(Senses .+)", "$1 $2; $3", RegexOptions.Compiled);
        cleanedText = Regex.Replace(cleanedText, @"(Base Atk .+);\s*\r?\n\s*(CMB .+);\s*\r?\n\s*(CMD .+)", "$1; $2; $3", RegexOptions.Compiled);
        cleanedText = Regex.Replace(cleanedText, @"D:\s*domain spell;\s*\r?\n\s*Domains:\s*", "D domain spell; Domains ", RegexOptions.Compiled);

        // 6. Fix newlines before parenthesis (e.g., "(DC 19)") so they appear on the same line.
        cleanedText = Regex.Replace(cleanedText, @"\r?\n\s*(\([^)]+\))", " $1", RegexOptions.Compiled);

        // 7. Remove unwanted double quotes from spell names and descriptions.
        cleanedText = Regex.Replace(cleanedText, @"(?<!\w)""(.*?)""(?!\w)", "$1", RegexOptions.Compiled);

        // Merge any header block (e.g., "Breath Weapon (Su):", "Poison (Ex):", etc.)
        //    This generic regex finds a header (a line starting with a set of allowed characters followed by a colon)
        //    and then merges all following lines that do not themselves start with a header.
		cleanedText = Regex.Replace(cleanedText,
		@"(^[A-Za-z0-9 '()/-]+:\s*)([^\r\n]+(?:\r?\n(?![A-Za-z0-9 '()/-]+:\s|DESCRIPTION\b)[^\r\n]+)+)",
		m =>
		{
			string header = m.Groups[1].Value;
			string body = m.Groups[2].Value;
			return header + body.Replace("\r\n", " ").Replace("\n", " ");
		},
		RegexOptions.Multiline | RegexOptions.Compiled);

        // 9. Remove excessive blank lines.
        cleanedText = Regex.Replace(cleanedText, @"(\r?\n\s*){2,}", "\n", RegexOptions.Compiled).Trim();

        return cleanedText;
    }
}
