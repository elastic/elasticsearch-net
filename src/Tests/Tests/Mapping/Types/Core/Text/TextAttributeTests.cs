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

		[Text]
		public string Minimal { get; set; }

		public string Inferred { get; set; }
	}

	public class TextAttributeTests : AttributeTestsBase<TextTest>
	{
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
							type = "keyword",
							ignore_above = 256
						}
					}
				}
			}
		};
	}
}
