namespace Nest.Tests.Integration.Indices.Analysis.Analyzers
{
	public class AnalyzerTestResult
	{
		public IAnalyzeResponse AnalyzeResponse { get; set; }
		public IElasticType ElasticType { get; set; }
	}
}