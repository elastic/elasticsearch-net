namespace Nest.Litterateur.Documentation.Blocks
{
	public class TextBlock : IDocumentationBlock
	{
		public string Value { get; }
		public int LineNumber { get; }
		public TextBlock(string text, int lineNumber)
		{
			Value = text;
			LineNumber = lineNumber;
		}
	}
}