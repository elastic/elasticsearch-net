using System;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public interface IGeoPointProperty : IProperty
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

		[JsonProperty("fielddata")]
		IGeoPointFielddata Fielddata { get; set; }
	}

	public class GeoPointProperty : PropertyBase, IGeoPointProperty
	{
		public GeoPointProperty() : base("geo_point") { }

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
		
		public GeoPointPropertyDescriptor<T> LatLon(bool latLon = true) => Assign(a => a.LatLon = latLon);

		public GeoPointPropertyDescriptor<T> GeoHash(bool geoHash = true) => Assign(a => a.GeoHash = geoHash);

		public GeoPointPropertyDescriptor<T> GeoHashPrecision(int geoHashPrecision) => Assign(a => a.GeoHashPrecision = geoHashPrecision);

		public GeoPointPropertyDescriptor<T> GeoHashPrefix(bool geoHashPrefix = true) => Assign(a => a.GeoHashPrefix = geoHashPrefix);

		public GeoPointPropertyDescriptor<T> Validate(bool validate = true) => Assign(a => a.Validate = validate);

		public GeoPointPropertyDescriptor<T> ValidateLongitude(bool validateLatitude = true) => Assign(a => a.ValidateLatitude = validateLatitude);

		public GeoPointPropertyDescriptor<T> ValidateLatitude(bool validateLongitude = true) => Assign(a => a.ValidateLongitude = validateLongitude);

		public GeoPointPropertyDescriptor<T> Normalize(bool normalize = true) => Assign(a => a.Normalize = normalize);

		public GeoPointPropertyDescriptor<T> NormalizeLatitude(bool normalizeLatitude = true) => Assign(a => a.NormalizeLatitude = normalizeLatitude);

		public GeoPointPropertyDescriptor<T> NormalizeLongitude(bool normalizeLongitude = true) => Assign(a => a.NormalizeLongitude = normalizeLongitude);

		public GeoPointPropertyDescriptor<T> PrecisionStep(int precisionStep) => Assign(a => a.PrecisionStep = precisionStep);

		public GeoPointPropertyDescriptor<T> Fielddata(Func<GeoPointFielddataDescriptor, IGeoPointFielddata> selector) =>
			Assign(a => a.Fielddata = selector(new GeoPointFielddataDescriptor()));
	}
}