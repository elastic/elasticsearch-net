// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Nest;

namespace Tests.Mapping.Types.Core.Binary
{
	public class BinaryTest
	{
		[Binary(
			DocValues = true,
			Similarity = "classic",
			Store = true)]
		public string Full { get; set; }

		[Binary]
		public string Minimal { get; set; }
	}

	public class BinaryAttributeTests : AttributeTestsBase<BinaryTest>
	{
		protected override object ExpectJson => new
		{
			properties = new
			{
				full = new
				{
					type = "binary",
					doc_values = true,
					similarity = "classic",
					store = true
				},
				minimal = new
				{
					type = "binary"
				}
			}
		};
	}
}
