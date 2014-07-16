using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
    public class DynamicTemplate
    {
		[JsonProperty("match")]
		public string Match { get; set; }

		[JsonProperty("unmatch")]
		public string Unmatch { get; set; }

		[JsonProperty("match_mapping_type")]
		public string MatchMappingType { get; set; }

		[JsonProperty("path_match")]
		public string PathMatch { get; set; }

		[JsonProperty("path_unmatch")]
		public string PathUnmatch { get; set; }

		[JsonProperty("mapping"), JsonConverter(typeof(ElasticTypeConverter))]
		public IElasticType Mapping { get; set; }

    }
}