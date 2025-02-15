using HtmlAgilityPack;
using System;
using System.Text;
using System.Text.RegularExpressions;

namespace AonCreatureExtractor.methods {
	public static partial class HtmlParser
	{
		[GeneratedRegex(@"[ \t]+(\r?\n)", RegexOptions.Compiled)]
        private static partial Regex TrailingSpacesRegex();
		
		public static (string Text, bool IsNpc) ExtractText(string html)
		{
			if (string.IsNullOrWhiteSpace(html))
				return (string.Empty, false);

			var		doc = new HtmlDocument();
			doc.LoadHtml(html);

			var		contentNode = doc.DocumentNode.SelectSingleNode("//table[@id='MainContent_DataListFeats']")
								?? doc.DocumentNode.SelectSingleNode("//table[@id='MainContent_DataListNPCs']");
			if (contentNode == null)
				return ("No valid content found.", false);

			bool	isNpc = contentNode.Id.Equals("MainContent_DataListNPCs", StringComparison.OrdinalIgnoreCase);

			string	text = ExtractFormattedText(contentNode);
			
			return (text, isNpc);
		}

		private static string ExtractFormattedText(HtmlNode node)
		{
			var sb = new StringBuilder();

			foreach (var child in node.ChildNodes)
			{
				switch (child.Name.ToLower())
				{
					case "h1":
						break;
					case "h2":
					case "h3":
						sb.AppendLine();
						sb.AppendLine(child.InnerText.Trim());
						break;
					case "br":
						sb.AppendLine();
						break;
					case "b":
						sb.Append(ExtractFormattedText(child).TrimEnd());
						sb.Append(' ');
						break;
					default:
						if (child.NodeType == HtmlNodeType.Text)
						{
							sb.Append(child.InnerText.TrimEnd());
							sb.Append(' ');
						}
						else
						{
							sb.Append(ExtractFormattedText(child));
						}
						break;
				}
			}
			string result = sb.ToString();
			result = TrailingSpacesRegex().Replace(result, "$1");
			return result.TrimStart('\r', '\n', ' ').TrimEnd();
		}
	}
}
