using HtmlAgilityPack;
using System;
using System.Text;

/// <summary>
/// Provides methods for parsing HTML content and extracting formatted text.
/// </summary>
public static class HtmlParser
{
	/// <summary>
	/// Extracts text from the provided HTML string by parsing it and processing its nodes.
	/// It specifically looks for content within a table element with the id 'MainContent_DataListFeats'.
	/// </summary>
	/// <param name="html">The HTML content to parse.</param>
	/// <returns>
	/// A formatted string extracted from the HTML. Returns an empty string if the input is null or whitespace,
	/// or a message if the specified content node is not found.
	/// </returns>
	public static string ExtractText(string html)
	{
		// Return an empty string if the input HTML is null, empty, or whitespace.
		if (string.IsNullOrWhiteSpace(html))
			return string.Empty;

		// Create an instance of HtmlDocument and load the HTML content.
		var doc = new HtmlDocument();
		doc.LoadHtml(html);

		// Select the content node using XPath.
		// This example targets a table with the specific id 'MainContent_DataListFeats'.
		var contentNode = doc.DocumentNode.SelectSingleNode("//table[@id='MainContent_DataListFeats']");
		if (contentNode == null)
			return "No valid content found.";

		// Extract and format the text from the content node.
		string text = ExtractFormattedText(contentNode);

		return text;
	}

	/// <summary>
	/// Recursively extracts and formats text from the provided HtmlNode and its child nodes.
	/// Handles header tags (h1, h2, h3), break elements, and text nodes appropriately.
	/// </summary>
	/// <param name="node">The HtmlNode from which to extract text.</param>
	/// <returns>A string containing the formatted text extracted from the node.</returns>
	private static string ExtractFormattedText(HtmlNode node)
	{
		// Use a StringBuilder for efficient concatenation of strings.
		var sb = new StringBuilder();

		// Iterate through each child node of the current node.
		foreach (var child in node.ChildNodes)
		{
			// Process the node based on its tag name (converted to lowercase for consistency).
			switch (child.Name.ToLower())
			{
				// For header elements, add a newline before and after, and convert text to uppercase.
				case "h1":
				case "h2":
				case "h3":
					sb.AppendLine();                      // Insert a newline to separate from previous content.
					sb.AppendLine(child.InnerText.ToUpper()); // Append header text in uppercase followed by a newline.
					break;

				// For <br> elements, simply insert a newline.
				case "br":
					sb.AppendLine();
					break;

				// Default case for other node types.
				default:
					// If the node is a text node, append its text content with an extra space.
					if (child.NodeType == HtmlNodeType.Text)
					{
						sb.Append(child.InnerText + " ");
					}
					else
					{
						// For non-text element nodes, recursively extract their formatted text.
						sb.Append(ExtractFormattedText(child));
					}
					break;
			}
		}

		// Return the built string after trimming any extra whitespace from the start and end.
		return sb.ToString().Trim();
	}
}
