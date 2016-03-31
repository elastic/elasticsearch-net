using System;
using Nest;

namespace Tests.Mapping.Types.Core.Text
{
	public class TextTest
	{
		[Text(
			Analyzer = "myanalyzer",
			Boost = 1.2,
			EagerGlobalOrdinals = true,
			Fielddata = true,
			IncludeInAll = false,
			Index = true,
			IndexOptions = IndexOptions.Offsets,
			PositionIncrementGap = 5,
			SearchAnalyzer = "mysearchanalyzer",
			SearchQuoteAnalyzer = "mysearchquoteanalyzer",
			Similarity = SimilarityOption.Classic,
			Store = true)]
		public string Full { get; set; }

		[Text]
		public string Minimal { get; set; }

        public string Inferred { get; set; }
	}

	public class TextMappingTests : TypeMappingTestBase<TextTest>
	{
		protected override bool AutoMap => true;

		protected override object ExpectJson => new
		{
			properties = new
			{
				full = new
				{
					type = "text",
					analyzer = "myanalyzer",
					boost = 1.2,
					eager_global_ordinals = true,
					fielddata = true,
					include_in_all = false,
					index = true,
					index_options = "offsets",
					position_increment_gap = 5,
					search_analyzer = "mysearchanalyzer",
					search_quote_analyzer = "mysearchquoteanalyzer",
					similarity = "classic",
					store = true
				},
				minimal = new
				{
					type = "text"
				},
                inferred = new
				{
					type = "text",
					fields = new
					{
						keyword = new
						{
							type = "keyword"
						}
					}
				}
			}
		};

		protected override Func<PropertiesDescriptor<TextTest>, IPromise<IProperties>> FluentProperties => p => p
			.Text(t => t
				.Name(o => o.Full)
				.Analyzer("myanalyzer")
				.Boost(1.2)
				.EagerGlobalOrdinals()
				.Fielddata()
				.IncludeInAll(false)
				.Index()
				.IndexOptions(IndexOptions.Offsets)
				.PositionIncrementGap(5)
				.SearchAnalyzer("mysearchanalyzer")
				.SearchQuoteAnalyzer("mysearchquoteanalyzer")
				.Similarity(SimilarityOption.Classic)
				.Store()
			)
			.Text(s => s
				.Name(o => o.Minimal)
			);

		protected override object ExpectJsonFluentOnly => new
		{
			properties = new
			{
				full = new
				{
					type = "text",
					norms = new
					{
						enabled = true,
						loading = "lazy"
					}
				}
			}
		};
		protected override Func<PropertiesDescriptor<TextTest>, IPromise<IProperties>> FluentOnlyProperties => p => p
			.Text(s => s
				.Name(o => o.Full)
				.Norms(n => n
					.Enabled()
					.Loading(NormsLoading.Lazy)
				)
			);
	}
}
