using System.Collections.Generic;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using System;
using Newtonsoft.Json.Converters;

namespace Nest
{
	public class GeoShapeMapping : IElasticType
	{
		[JsonIgnore]
		public string Name { get; set; }

		private string __type;
		[JsonProperty("type")]
		public virtual string Type { get { return __type ?? "point"; } set { __type = value; } }

    [JsonProperty("similarity")]
    public string Similarity { get; set; }

		[JsonProperty("tree"), JsonConverter(typeof(StringEnumConverter))]
		public GeoTree? Tree { get; set; }

		[JsonProperty("tree_levels")]
		public int? TreeLevels { get; set; }

		[JsonProperty("distance_error_pct")]
		public double? DistanceErrorPercentage { get; set; }
	}
}