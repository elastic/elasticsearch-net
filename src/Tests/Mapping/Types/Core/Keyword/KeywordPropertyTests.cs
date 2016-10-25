using System;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.MockData;
using Tests.Framework.Integration;

namespace Tests.Mapping.Types.Core.Keyword
{
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
				.IncludeInAll()
				.Index(false)
				.IndexOptions(IndexOptions.Freqs)
				.NullValue("null")
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
					IncludeInAll = true,
					Index = false,
					IndexOptions = IndexOptions.Freqs,
					NullValue = "null",
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
