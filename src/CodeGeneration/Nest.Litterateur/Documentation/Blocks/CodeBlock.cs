namespace Nest.Litterateur.Documentation.Blocks
{
	public class CodeBlock : IDocumentationBlock
	{
		public string Value { get; }
		public int LineNumber { get; }
		public CodeBlock(string lineOfCode, int lineNumber)
		{
			Value = lineOfCode.Trim();
			LineNumber = lineNumber;
		}
	}
}