using System.Collections.Generic;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using System;
using Newtonsoft.Json.Converters;
using Nest.Resolvers;

namespace Nest
{
	public class GeoShapeMapping : IElasticType
	{
		[JsonIgnore]
		public TypeNameMarker TypeNameMarker { get; set; }

    [JsonProperty(PropertyName = "name")]
    public string Name { get; set; }

		private TypeNameMarker __type;
		[JsonProperty("type")]
		public virtual TypeNameMarker Type { get { return (TypeNameMarker)(__type ?? "point"); } set { __type = value; } }

		[JsonProperty("tree"), JsonConverter(typeof(StringEnumConverter))]
		public GeoTree? Tree { get; set; }

		[JsonProperty("tree_levels")]
		public int? TreeLevels { get; set; }

		[JsonProperty("distance_error_pct")]
		public double? DistanceErrorPercentage { get; set; }
	}
}