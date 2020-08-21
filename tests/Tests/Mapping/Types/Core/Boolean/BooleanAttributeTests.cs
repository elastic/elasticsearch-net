// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Nest;

namespace Tests.Mapping.Types.Core.Boolean
{
	public class BooleanTest
	{
		[Boolean(
			DocValues = false,
			Similarity = "BM25",
			Index = false,
			Store = true)]
		public bool Full { get; set; }

		[Boolean]
		public bool Minimal { get; set; }
	}

	public class BooleanAttributeTests : AttributeTestsBase<BooleanTest>
	{
		protected override object ExpectJson => new
		{
			properties = new
			{
				full = new
				{
					type = "boolean",
					doc_values = false,
					similarity = "BM25",
					store = true,
					index = false,
				},
				minimal = new
				{
					type = "boolean"
				}
			}
		};
	}
}
