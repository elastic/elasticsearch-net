using Nest;

namespace Tests.Mapping.Types.Complex.Flattened
{
	public class FlattenedTest
	{
		[Flattened(
			Boost = 2,
			IgnoreAbove = 256,
			Index = true,
			Similarity = "BM25",
			DepthLimit = 5,
			DocValues = true,
			IndexOptions = IndexOptions.Freqs,
			NullValue = "N/A",
			EagerGlobalOrdinals = true,
			SplitQueriesOnWhitespace = true)]
		public InnerObject Full { get; set; }

		[Flattened]
		public InnerObject Minimal { get; set; }

		public class InnerObject
		{
			public string Name { get; set; }
		}
	}

	public class FlattenedAttributeTests : AttributeTestsBase<FlattenedTest>
	{
		protected override object ExpectJson => new
		{
			properties = new
			{
				full = new
				{
					type = "flattened",
					boost = 2.0,
					ignore_above = 256,
					index = true,
					similarity = "BM25",
					depth_limit = 5,
					doc_values = true,
					index_options = "freqs",
					null_value = "N/A",
					eager_global_ordinals = true,
					split_queries_on_whitespace = true
				},
				minimal = new
				{
					type = "flattened"
				}
			}
		};
	}
}
