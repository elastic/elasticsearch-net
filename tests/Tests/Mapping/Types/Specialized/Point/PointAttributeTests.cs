// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

 using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;

namespace Tests.Mapping.Types.Specialized.Point
{
	public class PointTest
	{
		[Point(IgnoreMalformed = true, IgnoreZValue = true)]
		public object Full { get; set; }

		[Point]
		public object Minimal { get; set; }
	}

	[SkipVersion("<7.8.0", "Points introduced in 7.8.0+")]
	public class PointAttributeTests : AttributeTestsBase<PointTest>
	{
		protected override object ExpectJson => new
		{
			properties = new
			{
				full = new
				{
					type = "point",
					ignore_z_value = true,
					ignore_malformed = true

				},
				minimal = new
				{
					type = "point"
				}
			}
		};
	}
}
