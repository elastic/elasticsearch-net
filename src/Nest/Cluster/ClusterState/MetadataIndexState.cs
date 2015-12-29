using System;
using System.Linq;
using System.Collections.Generic;
using Elasticsearch.Net;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Nest
{
	public class MetadataIndexState
	{
		[JsonProperty("state")]
		public string State { get; internal set; }

		[JsonProperty("settings")]
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter))]
		public DynamicResponse Settings { get; internal set; }

		[JsonProperty("mappings")]
		public IMappings Mappings { get; internal set; }

		[JsonProperty("aliases")]
		public IEnumerable<string> Aliases { get; internal set; }
	}
}