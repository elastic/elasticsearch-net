// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Nest;

namespace Tests.Mapping.Types.Specialized.Murmur3Hash
{
	public class Murmur3HashTest
	{
		[Murmur3Hash]
		public string Full { get; set; }

		[Murmur3Hash]
		public string Minimal { get; set; }
	}

	public class Murmur3HashAttributeTests : AttributeTestsBase<Murmur3HashTest>
	{
		protected override object ExpectJson => new
		{
			properties = new
			{
				full = new
				{
					type = "murmur3",
				},
				minimal = new
				{
					type = "murmur3"
				}
			}
		};
	}
}
