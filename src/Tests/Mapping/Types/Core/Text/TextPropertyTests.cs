using System;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.MockData;
using Tests.Framework.Integration;

namespace Tests.Mapping.Types.Core.Text
{
	public class TextPropertyTests : PropertyTestsBase
	{
		public TextPropertyTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson => new
		{
			properties = new
			{
				name = new
				{
					type = "text",
					analyzer = "standard",
					boost = 1.2,
					eager_global_ordinals = true,
					fielddata = true,
					include_in_all = false,
					index = true,
					index_options = "offsets",
					position_increment_gap = 5,
					search_analyzer = "standard",
					search_quote_analyzer = "standard",
					similarity = "classic",
					store = true,
					norms = false
				}
			}
		};


		protected override Func<PropertiesDescriptor<Project>, IPromise<IProperties>> FluentProperties => f => f
				.Text(s => s
					.Name(p => p.Name)
					.Analyzer("standard")
					.Boost(1.2)
					.EagerGlobalOrdinals()
					.Fielddata()
					.IncludeInAll(false)
					.Index(true)
					.IndexOptions(IndexOptions.Offsets)
					.PositionIncrementGap(5)
					.SearchAnalyzer("standard")
					.SearchQuoteAnalyzer("standard")
					.Similarity(SimilarityOption.Classic)
					.Store()
					.Norms(false)
				);


		protected override IProperties InitializerProperties => new Properties
		{
			{ "name", new TextProperty
				{
					Analyzer = "standard",
					Boost = 1.2,
					EagerGlobalOrdinals = true,
					Fielddata = true,
					IncludeInAll = false,
					Index = true,
					IndexOptions = IndexOptions.Offsets,
					PositionIncrementGap = 5,
					SearchAnalyzer = "standard",
					SearchQuoteAnalyzer = "standard",
					Similarity = SimilarityOption.Classic,
					Store = true,
					Norms = false
				}
			}
		};
	}
}
