using System;
using Nest;

namespace Tests.Mapping.Types.Geo.GeoPoint
{
	public class GeoPointTest
	{
		[GeoPoint(
			LatLon = true,
			GeoHash = true,
			GeoHashPrefix = true,
			GeoHashPrecision = 7,
			Validate = true,
			ValidateLatitude = true,
			ValidateLongitude = true,
			Normalize = true,
			NormalizeLatitude = true,
			NormalizeLongitude = true,
			PrecisionStep = 5)]
		public string Full { get; set; }

		[GeoPoint]
		public string Minimal { get; set; }

		public GeoLocation Inferred { get; set; }
	}

	public class GeoPointMappingTests : TypeMappingTestBase<GeoPointTest>
	{
		protected override object ExpectJson => new
		{
			properties = new
			{
				full = new
				{
					type = "geo_point",
					lat_lon = true,
					geohash = true,
					geohash_precision = 7,
					geohash_prefix = true,
					validate = true,
					validate_lat = true,
					validate_lon = true,
					normalize = true,
					normalize_lat = true,
					normalize_lon = true,
					precision_step = 5
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

		protected override Func<PropertiesDescriptor<GeoPointTest>, IPromise<IProperties>> FluentProperties => p => p
			.GeoPoint(s => s
				.Name(o => o.Full)
				.LatLon()
				.GeoHash()
				.GeoHashPrecision(7)
				.GeoHashPrefix()
				.Validate()
				.ValidateLatitude()
				.ValidateLongitude()
				.Normalize()
				.NormalizeLatitude()
				.NormalizeLongitude()
				.PrecisionStep(5)
			)
			.GeoPoint(b => b
				.Name(o => o.Minimal)
			)
			.GeoPoint(b => b
				.Name(o => o.Inferred)
			);
	}
}
