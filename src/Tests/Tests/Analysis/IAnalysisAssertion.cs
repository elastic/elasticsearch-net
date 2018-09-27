namespace Tests.Analysis.Tokenizers
{
	public interface IAnalysisAssertion
	{
		string Name { get; }
		object Json { get; }
	}
}