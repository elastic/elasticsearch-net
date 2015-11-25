using System;
using Nest;

namespace Tests.Mapping.Types.Core.Binary
{
	public class BinaryTest
	{
		[Binary(
			DocValues = true, 
			IndexName = "myindex", 
			Similarity = SimilarityOption.Default, 
			Store = true)]
		public string Full { get; set; }

		[Binary]
		public string Minimal { get; set; }
	}

	public class BinaryMappingTests : TypeMappingTestBase<BinaryTest>
	{
		protected override object ExpectJson => new
		{
			properties = new
			{
				full = new
				{
					type = "binary",
					doc_values = true,
					index_name = "myindex",
					similarity = "default",
					store = true
				},
				minimal = new
				{
					type = "binary"
				}
			}
		};

		protected override Func<PropertiesDescriptor<BinaryTest>, IPromise<IProperties>> FluentProperties => p => p
			.Binary(s => s
				.Name(o => o.Full)
				.DocValues()
				.IndexName("myindex")
				.Similarity(SimilarityOption.Default)
				.Store()
			)
			.Binary(b => b
				.Name(o => o.Minimal)
			);
	}
}
