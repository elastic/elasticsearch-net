using System;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Analysis.CharFilters;
using Tests.Analysis.Normalizers;
using Tests.Analysis.TokenFilters;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.Integration;
using static Tests.Framework.Promisify;

namespace Tests.Mapping.Types.Core.Keyword
{
	[SkipVersion("<5.4.0", "This uses the normalizer feature introduced in 5.2.0, and word graph token filter from 5.4.0")]
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
					include_in_all = true,
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

		protected override ICreateIndexRequest CreateIndexSettings(CreateIndexDescriptor create) => create
			.Settings(s => s
				.Analysis(a => a
					.CharFilters(t => Promise(CharFilterUsageTests.FluentExample(s).Value.Analysis.CharFilters))
					.TokenFilters(t => Promise(TokenFilterUsageTests.FluentExample(s).Value.Analysis.TokenFilters))
					.Normalizers(t => Promise(NormalizerUsageTests.FluentExample(s).Value.Analysis.Normalizers))
				)
			);
#pragma warning disable 618 // Usage of IncludeInAll
		protected override Func<PropertiesDescriptor<Project>, IPromise<IProperties>> FluentProperties => f => f
			.Keyword(b => b
				.Name(p => p.State)
				.DocValues(false)
				.Boost(1.2)
				.EagerGlobalOrdinals()
				.IgnoreAbove(50)
				.IncludeInAll()
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
#pragma warning restore 618

		protected override IProperties InitializerProperties => new Properties
		{
			{
				"state", new KeywordProperty
				{
					DocValues = false,
					Boost = 1.2,
					EagerGlobalOrdinals = true,
					IgnoreAbove = 50,
#pragma warning disable 618
					IncludeInAll = true,
#pragma warning restore 618
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
