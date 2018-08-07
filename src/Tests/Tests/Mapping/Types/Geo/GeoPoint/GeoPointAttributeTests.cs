using System;
using Nest;

namespace Tests.Mapping.Types.Geo.GeoPoint
{
	public class GeoPointTest
	{
		[GeoPoint(
			IgnoreMalformed = true)]
		public string Full { get; set; }

		[GeoPoint]
		public string Minimal { get; set; }

		public GeoLocation Inferred { get; set; }
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
					ignore_malformed = true
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
