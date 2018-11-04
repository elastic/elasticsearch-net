namespace DocGenerator.Documentation.Blocks
{
	public class TextBlock : IDocumentationBlock
	{
		public TextBlock(string text, int lineNumber)
		{
			Value = text;
			LineNumber = lineNumber;
		}

		public int LineNumber { get; }

		public string Value { get; }

		public string ToAsciiDoc() => Value;
	}
}
