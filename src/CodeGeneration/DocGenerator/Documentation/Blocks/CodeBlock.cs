using System.Collections.Generic;

namespace DocGenerator.Documentation.Blocks
{
	public abstract class CodeBlock : IDocumentationBlock
	{
		protected readonly IList<string> Lines = new List<string>();

		protected CodeBlock(string text, int startingLine, int depth, string language, string memberName)
		{
			Lines.Add(text);
			LineNumber = startingLine;
			Depth = depth;
			Language = language;
			MemberName = memberName?.ToLowerInvariant() ?? string.Empty;
		}

		public int Depth { get; }

		public string Language { get; }

		public int LineNumber { get; }

		public string MemberName { get; }

		public string Value => string.Join(string.Empty, Lines);

		public abstract string ToAsciiDoc();

		public void AddLine(string line) => Lines.Add(line);
	}
}
