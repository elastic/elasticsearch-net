using System.Collections.Generic;
using Newtonsoft.Json;
using System;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public class GeoPointMapping : IElasticType
	{
		public PropertyNameMarker Name { get; set; }

		[JsonProperty("type")]
		public virtual TypeNameMarker Type { get { return new TypeNameMarker { Name = "geo_point" }; } }

		[JsonProperty("similarity")]
		public string Similarity { get; set; }

		[JsonProperty("lat_lon")]
		public bool? IndexLatLon { get; set; }

		[JsonProperty("geohash")]
		public bool? IndexGeoHash { get; set; }

        [JsonProperty("geohash_prefix")]
        public bool? GeoHashPrefix { get; set; }

		[JsonProperty("geohash_precision")]
		public int? GeoHashPrecision { get; set; }

		[JsonProperty("fielddata")]
		public FieldDataNonStringMapping FieldData { get; set; }
	}
}