using System.Linq;
using Elastic.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Analysis.Tokenizers;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Xunit;
using static Tests.Framework.Promisify;

namespace Tests.Analysis
{

	[SkipVersion("<5.2.0", "Normalizers are a new 5.2.0 feature")]
	public class AnalysisWithNormalizerCrudTests : AnalysisCrudTests
	{
		public AnalysisWithNormalizerCrudTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override CreateIndexRequest CreateInitializer(string indexName) => new CreateIndexRequest(indexName)
		{
			Settings = new IndexSettings
			{
				Analysis = new Nest.Analysis
				{
					Analyzers = Analyzers.AnalyzerUsageTests.InitializerExample.Analysis.Analyzers,
					CharFilters = CharFilters.CharFilterUsageTests.InitializerExample.Analysis.CharFilters,
					Tokenizers = AnalysisUsageTests.TokenizersInitializer.Analysis.Tokenizers,
					TokenFilters = AnalysisUsageTests.TokenFiltersInitializer.Analysis.TokenFilters,
					Normalizers = Normalizers.NormalizerUsageTests.InitializerExample.Analysis.Normalizers,
				}
			}
		};

		protected override ICreateIndexRequest CreateFluent(string indexName, CreateIndexDescriptor c) =>
			c.Settings(s => s
				.Analysis(a => a
					.Analyzers(t => Promise(Analyzers.AnalyzerUsageTests.FluentExample(s).Value.Analysis.Analyzers))
					.CharFilters(t => Promise(CharFilters.CharFilterUsageTests.FluentExample(s).Value.Analysis.CharFilters))
					.Tokenizers(t => Promise(AnalysisUsageTests.TokenizersFluent.Analysis.Tokenizers))
					.TokenFilters(t => Promise(AnalysisUsageTests.TokenFiltersFluent.Analysis.TokenFilters))
					.Normalizers(t => Promise(Normalizers.NormalizerUsageTests.FluentExample(s).Value.Analysis.Normalizers))
				)
			);
	}
}
