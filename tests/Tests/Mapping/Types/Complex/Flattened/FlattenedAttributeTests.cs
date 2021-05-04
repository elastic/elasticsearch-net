// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Nest;

namespace Tests.Mapping.Types.Complex.Flattened
{
	public class FlattenedTest
	{
		[Flattened(
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
