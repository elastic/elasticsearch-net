// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Nest;

namespace Tests.Mapping.Types.Geo.GeoPoint
{
	public class GeoPointTest
	{
		[GeoPoint(IgnoreMalformed = true, IgnoreZValue = true)]
		public string Full { get; set; }

		public GeoLocation Inferred { get; set; }

		[GeoPoint]
		public string Minimal { get; set; }
	}

	public class GeoPointAttributeTests : AttributeTestsBase<GeoPointTest>
	{
		protected override object ExpectJson => new
		{
			properties = new
			{
				full = new
				{
					type = "geo_point",
					ignore_malformed = true,
					ignore_z_value = true
				},
				minimal = new
				{
					type = "geo_point"
				},
				inferred = new
				{
					type = "geo_point"
				}
			}
		};
	}
}
