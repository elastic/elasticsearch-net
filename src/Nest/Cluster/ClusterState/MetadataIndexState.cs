using System.Collections.Generic;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	public class MetadataIndexState
	{
		[JsonProperty("aliases")]
		public IEnumerable<string> Aliases { get; internal set; }

		[JsonProperty("mappings")]
		public IMappings Mappings { get; internal set; }

		[JsonProperty("settings")]
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter<string, object>))]
		public DynamicResponse Settings { get; internal set; }

		[JsonProperty("state")]
		public string State { get; internal set; }
	}
}
