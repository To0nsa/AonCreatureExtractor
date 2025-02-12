using HtmlAgilityPack;
using System;
using System.Text;

public static class HtmlParser
{
	public static string ExtractText(string html)
	{
		if (string.IsNullOrWhiteSpace(html))
			return string.Empty;

		var doc = new HtmlDocument();
		doc.LoadHtml(html);

		var contentNode = doc.DocumentNode.SelectSingleNode("//table[@id='MainContent_DataListFeats']");
		if (contentNode == null)
			return "No valid content found.";

		string text = ExtractFormattedText(contentNode);

		return TextProcessor.FormatText(text);
	}

	private static string ExtractFormattedText(HtmlNode node)
	{
		var sb = new StringBuilder();

		foreach (var child in node.ChildNodes)
		{
			switch (child.Name.ToLower())
			{
				case "h1":
				case "h2":
				case "h3":
					sb.AppendLine();
					sb.AppendLine(child.InnerText.ToUpper());
					break;
				case "b":
					sb.AppendLine();
					sb.Append(child.InnerText + ": ");
					break;
				case "i":
					sb.AppendLine($"\"{child.InnerText}\"");
					break;
				case "br":
					sb.AppendLine();
					break;
				default:
					if (child.NodeType == HtmlNodeType.Text)
					{
						sb.Append(child.InnerText + " ");
					}
					else
					{
						sb.Append(ExtractFormattedText(child));
					}
					break;
			}
		}

		return sb.ToString().Trim();
	}
}
