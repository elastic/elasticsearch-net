using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public class GeoShapeMapping : IElasticType
	{
		public PropertyNameMarker Name { get; set; }

		[JsonProperty("type")]
		public virtual TypeNameMarker Type { get { return new TypeNameMarker { Name = "geo_shape" }; } }

		[JsonProperty("similarity")]
		public string Similarity { get; set; }

		[JsonProperty("tree"), JsonConverter(typeof(StringEnumConverter))]
		public GeoTree? Tree { get; set; }

		[JsonProperty("precision")]
		public GeoPrecision Precision { get; set; }

		[JsonProperty("orientation")]
		public GeoOrientation? Orientation { get; set; }

		[JsonProperty("tree_levels")]
		public int? TreeLevels { get; set; }

		[JsonProperty("distance_error_pct")]
		public double? DistanceErrorPercentage { get; set; }
	}
}