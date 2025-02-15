namespace AonCreatureExtractor.methods {
	public static class Logger
	{
		public static void PrintHeader(string title)
		{
			Console.WriteLine(Environment.NewLine + new string('-', 70));
			Console.WriteLine(title);
			Console.WriteLine(new string('-', 70));
		}

		public static void PrintText(string text, int? length = null)
		{
			if (length.HasValue && text.Length > length.Value)
				Console.WriteLine(string.Concat(text.AsSpan(0, length.Value), "..."));
			else
				Console.WriteLine(text);
		}
	}
}

