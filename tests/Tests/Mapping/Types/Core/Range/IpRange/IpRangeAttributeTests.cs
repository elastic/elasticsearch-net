// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;

namespace Tests.Mapping.Types.Core.Range.IpRange
{
	public class IpRangeTest
	{
		[IpRange]
		public IpAddressRange Range { get; set; }
	}

	[SkipVersion("<5.5.0", "ip range type is a new 5.5.0 feature")]
	public class IpRangeAttributeTests : AttributeTestsBase<IpRangeTest>
	{
		protected override object ExpectJson => new
		{
			properties = new
			{
				range = new
				{
					type = "ip_range"
				}
			}
		};
	}
}
