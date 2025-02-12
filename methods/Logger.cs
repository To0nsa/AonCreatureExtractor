using System;

public static class Logger
{
	public static void PrintHeader(string title)
	{
		Console.WriteLine(Environment.NewLine + new string('-', 40));
		Console.WriteLine(title);
		Console.WriteLine(new string('-', 40));
	}

	public static void PrintText(string text, int? length = null)
	{
		if (length.HasValue && text.Length > length.Value)
			Console.WriteLine(text.Substring(0, length.Value) + "...");
		else
			Console.WriteLine(text);
	}
}
