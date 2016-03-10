using System;

namespace Nest.Litterateur.Documentation.Blocks
{
	public class CodeBlock : IDocumentationBlock
	{
		public string Value { get; }
		public int LineNumber { get; }

		public string Language { get; set; }

		public CodeBlock(string lineOfCode, int lineNumber, string language)
		{
			if (language == null)
			{
				throw new ArgumentNullException(nameof(language));
			}

			Value = lineOfCode.Trim();
			LineNumber = lineNumber;
			Language = language;
		}
	}
}