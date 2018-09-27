using System;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using Nest;
using Tests.Analysis.Tokenizers;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using static Tests.Framework.Promisify;

namespace Tests.Mapping.Types.Core.Keyword
{
	[SkipVersion("<5.2.0", "This uses the normalizer feature introduced in 5.2.0")]
	public class KeywordPropertyTests : PropertyTestsBase
	{
		public KeywordPropertyTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override ICreateIndexRequest CreateIndexSettings(CreateIndexDescriptor create) => create
			.Settings(s => s
				.Analysis(a => a
					.CharFilters(t => Promise(Analysis.CharFilters.CharFilterUsageTests.FluentExample(s).Value.Analysis.CharFilters))
					.TokenFilters(t => Promise(AnalysisUsageTests.TokenFiltersFluent.Analysis.TokenFilters))
					.Normalizers(t => Promise(Analysis.Normalizers.NormalizerUsageTests.FluentExample(s).Value.Analysis.Normalizers))
				)
			);

		protected override object ExpectJson => new
		{
			properties = new
			{
				state = new
				{
					type = "keyword",
					doc_values = false,
					boost = 1.2,
					eager_global_ordinals = true,
					ignore_above = 50,
					index = false,
					index_options = "freqs",
					null_value = "null",
					norms = false,
					similarity = "classic",
					fields = new
					{
						foo = new
						{
							type = "keyword",
							ignore_above = 10
						}
					},
					store = true,
					normalizer = "myCustom",
				}
			}
		};

		protected override Func<PropertiesDescriptor<Project>, IPromise<IProperties>> FluentProperties => f => f
			.Keyword(b => b
				.Name(p => p.State)
				.DocValues(false)
				.Boost(1.2)
				.EagerGlobalOrdinals()
				.IgnoreAbove(50)
				.Index(false)
				.IndexOptions(IndexOptions.Freqs)
				.NullValue("null")
				.Normalizer("myCustom")
				.Norms(false)
				.Similarity(SimilarityOption.Classic)
				.Store(true)
				.Fields(fs => fs
					.Keyword(k => k
						.Name("foo")
						.IgnoreAbove(10)
					)
				)
			);


		protected override IProperties InitializerProperties => new Properties
		{
			{ "state", new KeywordProperty
				{
					DocValues = false,
					Boost = 1.2,
					EagerGlobalOrdinals = true,
					IgnoreAbove = 50,
					Index = false,
					IndexOptions = IndexOptions.Freqs,
					NullValue = "null",
					Normalizer = "myCustom",
					Norms = false,
					Similarity = SimilarityOption.Classic,
					Store = true,
					Fields = new Properties
					{
						{ "foo", new KeywordProperty { IgnoreAbove = 10 } }
					}
				}
			}
		};
	}
}
