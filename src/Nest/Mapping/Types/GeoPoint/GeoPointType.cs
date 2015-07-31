using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using System.Linq.Expressions;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public interface IGeoPointType : IElasticType
	{
		[JsonProperty("lat_lon")]
		bool? LatLon { get; set; }

		[JsonProperty("geohash")]
		bool? GeoHash { get; set; }

		[JsonProperty("geohash_precision")]
		int? GeoHashPrecision { get; set; }

		[JsonProperty("geohash_prefix")]
        bool? GeoHashPrefix { get; set; }

		[JsonProperty("validate")]
		bool? Validate { get; set; }

		[JsonProperty("validate_lat")]
		bool? ValidateLatitude { get; set; }

		[JsonProperty("validate_lon")]
		bool? ValidateLongitude { get; set; }

		[JsonProperty("normalize")]
		bool? Normalize { get; set; }

		[JsonProperty("normalize_lat")]
		bool? NormalizeLatitude { get; set; }

		[JsonProperty("normalize_lon")]
		bool? NormalizeLongitude { get; set; }

		[JsonProperty("precision_step")]
		int? PrecisionStep { get; set; }
	}

	public class GeoPointType : ElasticType, IGeoPointType
	{
		public GeoPointType() : base("geo_point") { }

		public bool? LatLon { get; set; }
		public bool? GeoHash { get; set; }
        public bool? GeoHashPrefix { get; set; }
		public int? GeoHashPrecision { get; set; }
		public bool? Validate { get; set; }
		public bool? ValidateLatitude { get; set; }
		public bool? ValidateLongitude { get; set; }
		public bool? Normalize { get; set; }
		public bool? NormalizeLatitude { get; set; }
		public bool? NormalizeLongitude { get; set; }
		public int? PrecisionStep { get; set; }
	}

	public class GeoPointTypeDescriptor<T>
		: TypeDescriptorBase<GeoPointTypeDescriptor<T>, IGeoPointType, T>, IGeoPointType
		where T : class
	{
		bool? IGeoPointType.LatLon { get; set; }
		bool? IGeoPointType.GeoHash { get; set; }
		int? IGeoPointType.GeoHashPrecision { get; set; }
		bool? IGeoPointType.GeoHashPrefix { get; set; }
		bool? IGeoPointType.Validate { get; set; }
		bool? IGeoPointType.ValidateLatitude { get; set; }
		bool? IGeoPointType.ValidateLongitude { get; set; }
		bool? IGeoPointType.Normalize { get; set; }
		bool? IGeoPointType.NormalizeLatitude { get; set; }
		bool? IGeoPointType.NormalizeLongitude { get; set; }
		int? IGeoPointType.PrecisionStep { get; set; }

		public GeoPointTypeDescriptor<T> LatLon(bool latLon = true) => Assign(a => a.LatLon = latLon);

		public GeoPointTypeDescriptor<T> GeoHash(bool geoHash = true) => Assign(a => a.GeoHash = geoHash);

		public GeoPointTypeDescriptor<T> GeoHashPrecision(int geoHashPrecision) => Assign(a => a.GeoHashPrecision = geoHashPrecision);

		public GeoPointTypeDescriptor<T> GeoHashPrefix(bool geoHashPrefix = true) => Assign(a => a.GeoHashPrefix = geoHashPrefix);

		public GeoPointTypeDescriptor<T> Validate(bool validate) => Assign(a => a.Validate = validate);

		public GeoPointTypeDescriptor<T> ValidateLongitude(bool validateLatitude) => Assign(a => a.ValidateLatitude = validateLatitude);

		public GeoPointTypeDescriptor<T> ValidateLatitude(bool validateLongitude) => Assign(a => a.ValidateLongitude = validateLongitude);

		public GeoPointTypeDescriptor<T> Normalize(bool normalize) => Assign(a => a.Normalize = normalize);

		public GeoPointTypeDescriptor<T> NormalizeLatitude(bool normalizeLatitude) => Assign(a => a.NormalizeLatitude = normalizeLatitude);

		public GeoPointTypeDescriptor<T> NormalizeLongitude(bool normalizeLongitude) => Assign(a => a.NormalizeLongitude = normalizeLongitude);

		public GeoPointTypeDescriptor<T> PrecisionStep(int precisionStep) => Assign(a => a.PrecisionStep = precisionStep);
	}
}