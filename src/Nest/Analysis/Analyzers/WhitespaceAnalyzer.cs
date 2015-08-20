using System.Collections.Generic;

namespace Nest
{
	/// <summary>
	/// An analyzer of type whitespace that is built using a Whitespace Tokenizer.
	/// </summary>
	public interface IWhitespaceAnalyzer : IAnalyzer { } 

	public class WhitespaceAnalyzer : AnalyzerBase, IWhitespaceAnalyzer
	{
		public WhitespaceAnalyzer()
		{
			Type = "whitespace";
		}
	}

	public class WhitespaceAnalyzerDescriptor :
		AnalyzerDescriptorBase<WhitespaceAnalyzerDescriptor, IWhitespaceAnalyzer>, IWhitespaceAnalyzer
	{
		protected override string Type => "whitespace";
	}
}