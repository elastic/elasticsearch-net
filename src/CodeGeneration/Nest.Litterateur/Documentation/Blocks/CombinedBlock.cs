using System.Collections.Generic;

namespace Nest.Litterateur.Documentation.Blocks
{
	/// <summary>
	/// Used to keep a line of code (could be multiple e.g fluent syntax) and its annotations in one logical unit.
	/// So they do not suffer from reordering based on line number when writing out the documentation
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
