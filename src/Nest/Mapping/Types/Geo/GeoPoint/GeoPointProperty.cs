using System;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public interface IGeoPointProperty : IProperty
	{
		[JsonProperty("lat_lon")]
		[Obsolete("Deprecated in 2.3.0 and Removed in 5.0.0")]
		bool? LatLon { get; set; }

		[JsonProperty("geohash")]
		[Obsolete("Deprecated in 2.4.0 and Removed in 5.0.0")]
		bool? GeoHash { get; set; }

		[JsonProperty("geohash_precision")]
		[Obsolete("Deprecated in 2.4.0 and Removed in 5.0.0")]
		int? GeoHashPrecision { get; set; }

		[JsonProperty("geohash_prefix")]
		[Obsolete("Deprecated in 2.4.0 and Removed in 5.0.0")]
		bool? GeoHashPrefix { get; set; }

		[JsonProperty("validate")]
		[Obsolete("Removed in 5.0.0. Use IgnoreMalformed")]
		bool? Validate { get; set; }

		[JsonProperty("validate_lat")]
		[Obsolete("Removed in 5.0.0. Use IgnoreMalformed")]
		bool? ValidateLatitude { get; set; }

		[JsonProperty("validate_lon")]
		[Obsolete("Removed in 5.0.0. Use IgnoreMalformed")]
		bool? ValidateLongitude { get; set; }

		[JsonProperty("normalize")]
		[Obsolete("Removed in 5.0.0")]
		bool? Normalize { get; set; }

		[JsonProperty("normalize_lat")]
		[Obsolete("Removed in 5.0.0")]
		bool? NormalizeLatitude { get; set; }

		[JsonProperty("normalize_lon")]
		[Obsolete("Removed in 5.0.0")]
		bool? NormalizeLongitude { get; set; }

		[JsonProperty("precision_step")]
		[Obsolete("Removed in 5.0.0")]
		int? PrecisionStep { get; set; }

		[JsonProperty("fielddata")]
		[Obsolete("Removed in 5.0.0")]
		IGeoPointFielddata Fielddata { get; set; }
	}

	public class GeoPointProperty : PropertyBase, IGeoPointProperty
	{
		public GeoPointProperty() : base("geo_point") { }

		[Obsolete("Deprecated in 2.3.0 and Removed in 5.0.0")]
		public bool? LatLon { get; set; }
		[Obsolete("Deprecated in 2.4.0 and Removed in 5.0.0")]
		public bool? GeoHash { get; set; }
		[Obsolete("Deprecated in 2.4.0 and Removed in 5.0.0")]
		public bool? GeoHashPrefix { get; set; }
		[Obsolete("Deprecated in 2.4.0 and Removed in 5.0.0")]
		public int? GeoHashPrecision { get; set; }
		[Obsolete("Removed in 5.0.0. Use IgnoreMalformed")]
		public bool? Validate { get; set; }
		[Obsolete("Removed in 5.0.0. Use IgnoreMalformed")]
		public bool? ValidateLatitude { get; set; }
		[Obsolete("Removed in 5.0.0. Use IgnoreMalformed")]
		public bool? ValidateLongitude { get; set; }
		[Obsolete("Removed in 5.0.0")]
		public bool? Normalize { get; set; }
		[Obsolete("Removed in 5.0.0")]
		public bool? NormalizeLatitude { get; set; }
		[Obsolete("Removed in 5.0.0")]
		public bool? NormalizeLongitude { get; set; }
		[Obsolete("Removed in 5.0.0")]
		public int? PrecisionStep { get; set; }
		[Obsolete("Removed in 5.0.0")]
		public IGeoPointFielddata Fielddata { get; set; }
	}

	public class GeoPointPropertyDescriptor<T>
		: PropertyDescriptorBase<GeoPointPropertyDescriptor<T>, IGeoPointProperty, T>, IGeoPointProperty
		where T : class
	{
		bool? IGeoPointProperty.LatLon { get; set; }
		bool? IGeoPointProperty.GeoHash { get; set; }
		int? IGeoPointProperty.GeoHashPrecision { get; set; }
		bool? IGeoPointProperty.GeoHashPrefix { get; set; }
		bool? IGeoPointProperty.Validate { get; set; }
		bool? IGeoPointProperty.ValidateLatitude { get; set; }
		bool? IGeoPointProperty.ValidateLongitude { get; set; }
		bool? IGeoPointProperty.Normalize { get; set; }
		bool? IGeoPointProperty.NormalizeLatitude { get; set; }
		bool? IGeoPointProperty.NormalizeLongitude { get; set; }
		int? IGeoPointProperty.PrecisionStep { get; set; }
		IGeoPointFielddata IGeoPointProperty.Fielddata { get; set; }

		public GeoPointPropertyDescriptor() : base("geo_point") { }

		[Obsolete("Deprecated in 2.3.0 and Removed in 5.0.0")]
		public GeoPointPropertyDescriptor<T> LatLon(bool latLon = true) => Assign(a => a.LatLon = latLon);

		[Obsolete("Deprecated in 2.4.0 and Removed in 5.0.0")]
		public GeoPointPropertyDescriptor<T> GeoHash(bool geoHash = true) => Assign(a => a.GeoHash = geoHash);

		[Obsolete("Deprecated in 2.4.0 and Removed in 5.0.0")]
		public GeoPointPropertyDescriptor<T> GeoHashPrecision(int geoHashPrecision) => Assign(a => a.GeoHashPrecision = geoHashPrecision);

		[Obsolete("Deprecated in 2.4.0 and Removed in 5.0.0")]
		public GeoPointPropertyDescriptor<T> GeoHashPrefix(bool geoHashPrefix = true) => Assign(a => a.GeoHashPrefix = geoHashPrefix);

		[Obsolete("Removed in 5.0.0. Use IgnoreMalformed")]
		public GeoPointPropertyDescriptor<T> Validate(bool validate = true) => Assign(a => a.Validate = validate);

		[Obsolete("Removed in 5.0.0. Use IgnoreMalformed")]
		public GeoPointPropertyDescriptor<T> ValidateLongitude(bool validateLatitude = true) => Assign(a => a.ValidateLatitude = validateLatitude);

		[Obsolete("Removed in 5.0.0. Use IgnoreMalformed")]
		public GeoPointPropertyDescriptor<T> ValidateLatitude(bool validateLongitude = true) => Assign(a => a.ValidateLongitude = validateLongitude);

		[Obsolete("Removed in 5.0.0")]
		public GeoPointPropertyDescriptor<T> Normalize(bool normalize = true) => Assign(a => a.Normalize = normalize);

		[Obsolete("Removed in 5.0.0")]
		public GeoPointPropertyDescriptor<T> NormalizeLatitude(bool normalizeLatitude = true) => Assign(a => a.NormalizeLatitude = normalizeLatitude);

		[Obsolete("Removed in 5.0.0")]
		public GeoPointPropertyDescriptor<T> NormalizeLongitude(bool normalizeLongitude = true) => Assign(a => a.NormalizeLongitude = normalizeLongitude);

		[Obsolete("Removed in 5.0.0")]
		public GeoPointPropertyDescriptor<T> PrecisionStep(int precisionStep) => Assign(a => a.PrecisionStep = precisionStep);

		[Obsolete("Removed in 5.0.0")]
		public GeoPointPropertyDescriptor<T> Fielddata(Func<GeoPointFielddataDescriptor, IGeoPointFielddata> selector) =>
			Assign(a => a.Fielddata = selector(new GeoPointFielddataDescriptor()));
	}
}
