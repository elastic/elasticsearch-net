using Nest;

namespace Tests.Mapping.Types.Core.SearchAsYouType
{
	public class SearchAsYouTypeTest
	{
		[SearchAsYouType(
			MaxShingleSize = 4,
			Analyzer = "myanalyzer",
			Boost = 1.2,
			EagerGlobalOrdinals = true,
			Fielddata = true,
			Index = true,
			IndexOptions = IndexOptions.Offsets,
			PositionIncrementGap = 5,
			SearchAnalyzer = "mysearchanalyzer",
			SearchQuoteAnalyzer = "mysearchquoteanalyzer",
			Similarity = "classic",
			Store = true,
			Norms = false,
			TermVector = TermVectorOption.WithPositionsOffsets)]
		public string Full { get; set; }

		[SearchAsYouType(MaxShingleSize = 1)]
		public string MaxShingleSize { get; set; }

		[SearchAsYouType]
		public string Minimal { get; set; }
	}

	public class SearchAsYouTypeAttributeTests : AttributeTestsBase<SearchAsYouTypeTest>
	{
		protected override object ExpectJson => new
		{
			properties = new
			{
				full = new
				{
					type = "search_as_you_type",
					max_shingle_size = 4,
					analyzer = "myanalyzer",
					boost = 1.2,
					eager_global_ordinals = true,
					fielddata = true,
					index = true,
					index_options = "offsets",
					position_increment_gap = 5,
					search_analyzer = "mysearchanalyzer",
					search_quote_analyzer = "mysearchquoteanalyzer",
					similarity = "classic",
					store = true,
					norms = false,
					term_vector = "with_positions_offsets"
				},
				maxShingleSize = new
				{
					type = "search_as_you_type",
					max_shingle_size = 1
				},
				minimal = new
				{
					type = "search_as_you_type"
				}
			}
		};
	}
}
