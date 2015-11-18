using System;
using Nest;

namespace Tests.Mapping.Types.String
{
	public class StringTest
	{
		[String(
			Analyzer = "fooanalyzer",
			Boost = 1.2,
			DocValues = true,
			IgnoreAbove = 50,
			IncludeInAll = false,
			Index = FieldIndexOption.NotAnalyzed,
			IndexOptions = IndexOptions.Offsets,
			NullValue = "na",
			PositionOffsetGap = 5,
			SearchAnalyzer = "foosearchanalyzer",
			Similarity = SimilarityOption.BM25,
			Store = true,
			TermVector = TermVectorOption.WithPositionsOffsets)]
		public string StringProperty { get; set; }
	}

	public class StringMappingTests : TypeMappingTestBase<StringTest>
	{
		protected override object AttributeBasedValues => new
		{
			properties = new
			{
				stringProperty = new
				{
					type = "string",
					analyzer = "fooanalyzer",
					boost = 1.2,
					doc_values = true,
					ignore_above = 50,
					include_in_all = false,
					index = "not_analyzed",
					index_options = "offsets",
					null_value = "na",
					position_offset_gap = 5,
					search_analyzer = "foosearchanalyzer",
					similarity = "BM25",
					store = true,
					term_vector = "with_positions_offsets"
				}
			}
		};

		protected override object CodeBasedValues => new
		{
			properties = new
			{
				stringProperty = new
				{
					type = "string",
					analyzer = "baranalyzer",
					boost = 1.0,
					doc_values = false,
					ignore_above = 5,
					include_in_all = true,
					index = "no",
					index_options = "freqs",
					null_value = "null",
					position_offset_gap = 10,
					search_analyzer = "barsearchanalyzer",
					similarity = "default",
					store = false,
					term_vector = "no"
				}
			}
		};

		protected override Func<PropertiesDescriptor<StringTest>, IPromise<IProperties>> FluentProperties => p => p
			.String(s => s
				.Name(o => o.StringProperty)
				.Analyzer("baranalyzer")
				.Boost(1)
				.DocValues(false)
				.IgnoreAbove(5)
				.IncludeInAll()
				.Index(FieldIndexOption.No)
				.IndexOptions(IndexOptions.Freqs)
				.NullValue("null")
				.PositionOffsetGap(10)
				.SearchAnalyzer("barsearchanalyzer")
				.Similarity(SimilarityOption.Default)
				.Store(false)
				.TermVector(TermVectorOption.No)
			);
	}
}
