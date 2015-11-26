using System;
using Nest;

namespace Tests.Mapping.Types.Core.Boolean
{
	public class BooleanTest
	{
		[Boolean(
			DocValues = false, 
			IndexName = "myindex", 
			Similarity = SimilarityOption.BM25,
			Store = true)]
		public bool Full { get; set; }

		[Boolean]
		public bool Minimal { get; set; }
	}

	public class BooleanMappingTests : TypeMappingTestBase<BooleanTest>
	{
		protected override object ExpectJson => new
		{
			properties = new
			{
				full = new
				{
					type = "boolean",
					doc_values = false,
					index_name = "myindex",
					similarity = "BM25",
					store = true
				},
				minimal = new
				{
					type = "boolean"
				}
			}
		};

		protected override Func<PropertiesDescriptor<BooleanTest>, IPromise<IProperties>> FluentProperties => p => p
			.Boolean(s => s
				.Name(o => o.Full)
				.DocValues(false)
				.IndexName("myindex")
				.Similarity(SimilarityOption.BM25)
				.Store(true)
			)
			.Boolean(s => s
				.Name(o => o.Minimal)
			);
	}
}
