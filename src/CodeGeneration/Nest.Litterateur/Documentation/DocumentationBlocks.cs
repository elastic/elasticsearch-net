using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest.Litterateur.Documentation
{
	public interface IDocumentationBlock { string Value { get; } }

	public class TextBlock : IDocumentationBlock
	{
		public string Value { get; }
		public TextBlock(string text)
		{
			Value = text;
		}
	}

	public class CodeBlock : IDocumentationBlock
	{
		public string Value { get; }
		public CodeBlock(string lineOfCode)
		{
			Value = lineOfCode.Trim();
		}
	}

}
