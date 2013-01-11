using Newtonsoft.Json;

namespace Nest
{
    public class DynamicTemplate
    {
		[JsonProperty("match")]
		public string Match { get; internal set; }

		[JsonProperty("unmatch")]
		public string Unmatch { get; internal set; }

		[JsonProperty("match_mapping_type")]
		public string MatchMappingType { get; internal set; }

		[JsonProperty("path_match")]
		public string PathMatch { get; internal set; }

		[JsonProperty("path_unmatch")]
		public string PathUnmatch { get; internal set; }

		[JsonProperty("mapping")]
		public IElasticType Mapping { get; internal set; }

    }
}