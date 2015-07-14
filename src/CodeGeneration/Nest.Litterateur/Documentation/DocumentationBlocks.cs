using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest.Litterateur.Documentation
{
	public interface IDocumentationBlock
	{
		int LineNumber { get; }
		string Value { get; }
	}

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

	/// <summary>
	/// Used to keep a line of code (could be multiple e.g fluent syntax) and its annotations in one logical unit.
	/// So they do not suffer from reoordering based on line number when writing out the documentation
	/// </summary>
	public class CombinedBlock : IDocumentationBlock
	{
		public string Value { get; }
		public IEnumerable<IDocumentationBlock> Blocks { get; }
		public int LineNumber { get; }

		public CombinedBlock(IEnumerable<IDocumentationBlock> blocks, int lineNumber)
		{
			Blocks = blocks;
			LineNumber = lineNumber;
			Value = null;
		}
	}

}
