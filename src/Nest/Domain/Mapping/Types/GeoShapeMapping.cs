using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Nest.Resolvers;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public class GeoShapeMapping : IElasticType
	{
		public PropertyNameMarker Name { get; set; }

		private TypeNameMarker __type;
		[JsonProperty("type")]
		public virtual TypeNameMarker Type { get { return (TypeNameMarker)(__type ?? "point"); } set { __type = value; } }

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