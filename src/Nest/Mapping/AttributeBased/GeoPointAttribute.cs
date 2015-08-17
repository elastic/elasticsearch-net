using System;

namespace Nest
{
	public class GeoPointAttribute : ElasticPropertyAttribute
	{
		public bool LatLon { get; set; }
		public bool GeoHash { get; set; }
        public bool GeoHashPrefix { get; set; }
		public int GeoHashPrecision { get; set; }
		public bool Validate { get; set; }
		public bool ValidateLatitude { get; set; }
		public bool ValidateLongitude { get; set; }
		public bool Normalize { get; set; }
		public bool NormalizeLatitude { get; set; }
		public bool NormalizeLongitude { get; set; }
		public int PrecisionStep { get; set; }

		public override IElasticType ToElasticType() => new GeoPointType(this);
	}
}
