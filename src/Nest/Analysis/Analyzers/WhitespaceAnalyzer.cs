namespace Nest
{
	/// <summary>
	/// An analyzer of type whitespace that is built using a Whitespace Tokenizer.
	/// </summary>
	public interface IWhitespaceAnalyzer : IAnalyzer { } 

	/// <inheritdoc/>
	public class WhitespaceAnalyzer : AnalyzerBase, IWhitespaceAnalyzer
	{
		public WhitespaceAnalyzer() { Type = "whitespace"; }
	}

	/// <inheritdoc/>
	public class WhitespaceAnalyzerDescriptor :
		AnalyzerDescriptorBase<WhitespaceAnalyzerDescriptor, IWhitespaceAnalyzer>, IWhitespaceAnalyzer
	{
		protected override string Type => "whitespace";
	}
}