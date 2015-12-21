namespace Nest
{
	/// <summary>
	/// An analyzer of type keyword that “tokenizes” an entire stream as a single token. This is useful for data like zip codes, ids and so on. 
	/// <para>Note, when using mapping definitions, it make more sense to simply mark the field as not_analyzed.</para>
	/// </summary>
	public interface IKeywordAnalyzer : IAnalyzer { }
	/// <inheritdoc/>
	public class KeywordAnalyzer : AnalyzerBase, IKeywordAnalyzer
	{
		public KeywordAnalyzer() { Type = "keyword"; }
	}

	/// <inheritdoc/>
	public class KeywordAnalyzerDescriptor 
		: AnalyzerDescriptorBase<KeywordAnalyzerDescriptor, IKeywordAnalyzer>, IKeywordAnalyzer
	{
		protected override string Type => "keyword";
	}
}