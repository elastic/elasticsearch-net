using System.Collections.Generic;
using Elasticsearch.Net_5_2_0;
using Newtonsoft.Json;

namespace Nest_5_2_0
{
	public class MetadataIndexState
	{
		[JsonProperty("state")]
		public string State { get; internal set; }

		[JsonProperty("settings")]
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter<string, object>))]
		public DynamicResponse Settings { get; internal set; }

		[JsonProperty("mappings")]
		public IMappings Mappings { get; internal set; }

		[JsonProperty("aliases")]
		public IEnumerable<string> Aliases { get; internal set; }
	}
}
