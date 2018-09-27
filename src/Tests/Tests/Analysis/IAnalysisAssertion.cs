namespace Tests.Analysis
{
	public interface IAnalysisAssertion
	{
		string Name { get; }
		object Json { get; }
	}
}
