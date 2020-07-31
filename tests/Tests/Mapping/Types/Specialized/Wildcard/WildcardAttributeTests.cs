// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;

namespace Tests.Mapping.Types.Specialized.Wildcard
{
	public class WildcardTest
	{
		[Wildcard(IgnoreAbove = 512, NullValue = "foo")]
		public string Full { get; set; }

		[Wildcard]
		public string Simple { get; set; }
	}

	[SkipVersion("<7.9.0", "introduced in 7.9.0")]
	public class WildcardAttributeTests : AttributeTestsBase<WildcardTest>
	{
		protected override object ExpectJson => new
		{
			properties = new
			{
				full = new
				{
					type = "wildcard",
					ignore_above = 512,
					null_value = "foo"
				},
				simple = new
				{
					type = "wildcard"
				}
			}
		};
	}
}
