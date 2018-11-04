using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Analysis.Analyzers;
using Tests.Analysis.CharFilters;
using Tests.Analysis.Normalizers;
using Tests.Analysis.TokenFilters;
using Tests.Analysis.Tokenizers;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.Integration;
using static Tests.Framework.Promisify;

namespace Tests.Analysis
{
	[SkipVersion("<5.4.0", "Normalizers are a new 5.2.0 feature, but this tests also tries to send new 5.4.0 token filters")]
	public class AnalysisWithNormalizerCrudTests : AnalysisCrudTests
	{
		public AnalysisWithNormalizerCrudTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override CreateIndexRequest CreateInitializer(string indexName) => new CreateIndexRequest(indexName)
		{
			Settings = new IndexSettings
			{
				Analysis = new Nest.Analysis
				{
					Analyzers = AnalyzerUsageTests.InitializerExample.Analysis.Analyzers,
					CharFilters = CharFilterUsageTests.InitializerExample.Analysis.CharFilters,
					Tokenizers = TokenizerUsageTests.InitializerExample.Analysis.Tokenizers,
					TokenFilters = TokenFilterUsageTests.InitializerExample.Analysis.TokenFilters,
					Normalizers = NormalizerUsageTests.InitializerExample.Analysis.Normalizers,
				}
			}
		};

		protected override ICreateIndexRequest CreateFluent(string indexName, CreateIndexDescriptor c) =>
			c.Settings(s => s
				.Analysis(a => a
					.Analyzers(t => Promise(AnalyzerUsageTests.FluentExample(s).Value.Analysis.Analyzers))
					.CharFilters(t => Promise(CharFilterUsageTests.FluentExample(s).Value.Analysis.CharFilters))
					.Tokenizers(t => Promise(TokenizerUsageTests.FluentExample(s).Value.Analysis.Tokenizers))
					.TokenFilters(t => Promise(TokenFilterUsageTests.FluentExample(s).Value.Analysis.TokenFilters))
					.Normalizers(t => Promise(NormalizerUsageTests.FluentExample(s).Value.Analysis.Normalizers))
				)
			);
	}
}
