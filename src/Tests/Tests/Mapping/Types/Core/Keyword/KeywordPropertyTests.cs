using System;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Analysis;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.Integration;
using static Tests.Framework.Promisify;

namespace Tests.Mapping.Types.Core.Keyword
{
	[SkipVersion("<5.2.0", "This uses the normalizer feature introduced in 5.2.0")]
	public class KeywordPropertyTests : PropertyTestsBase
	{
		public KeywordPropertyTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

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
					similarity = "bm25",
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
				.Similarity(SimilarityOption.BM25)
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
			{
				"state", new KeywordProperty
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
					Similarity = SimilarityOption.BM25,
					Store = true,
					Fields = new Properties
					{
						{ "foo", new KeywordProperty { IgnoreAbove = 10 } }
					}
				}
			}
		};

		protected override ICreateIndexRequest CreateIndexSettings(CreateIndexDescriptor create) => create
			.Settings(s => s
				.Analysis(a => a
					.CharFilters(t => Promise(AnalysisUsageTests.CharFiltersFluent.Analysis.CharFilters))
					.TokenFilters(t => Promise(AnalysisUsageTests.TokenFiltersFluent.Analysis.TokenFilters))
					.Normalizers(t => Promise(AnalysisUsageTests.NormalizersInitializer.Analysis.Normalizers))
				)
			);

		[SkipVersion("<6.4.0", "split_queries_on_whitespace is a new option https://github.com/elastic/elasticsearch/pull/30691")]
		public class KeywordPropertySplitQueriesOnWhitespaceTests : PropertyTestsBase
		{
			public KeywordPropertySplitQueriesOnWhitespaceTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

			protected override object ExpectJson => new
			{
				properties = new
				{
					state = new
					{
						type = "keyword",
						split_queries_on_whitespace = true
					}
				}
			};

			protected override Func<PropertiesDescriptor<Project>, IPromise<IProperties>> FluentProperties => f => f
				.Keyword(b => b
					.Name(p => p.State)
					.SplitQueriesOnWhitespace()
				);


			protected override IProperties InitializerProperties => new Properties
			{
				{ "state", new KeywordProperty { SplitQueriesOnWhitespace = true } }
			};
		}
	}
}
