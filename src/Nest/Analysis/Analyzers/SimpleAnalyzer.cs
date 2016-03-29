namespace Nest
{
	/// <summary>
	/// An analyzer of type simple that is built using a Lower Case Tokenizer.
	/// </summary>
	public interface ISimpleAnalyzer : IAnalyzer { }

	/// <inheritdoc/>
	public class SimpleAnalyzer : AnalyzerBase, ISimpleAnalyzer
	{
		public SimpleAnalyzer() { Type = "simple"; }
	}

	/// <inheritdoc/>
	public class SimpleAnalyzerDescriptor :
		AnalyzerDescriptorBase<SimpleAnalyzerDescriptor, ISimpleAnalyzer>, ISimpleAnalyzer
	{
		protected override string Type => "simple";
	}
}