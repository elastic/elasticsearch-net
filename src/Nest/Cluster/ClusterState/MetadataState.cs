using System;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public class MetadataState
	{
		[JsonProperty("templates")]
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter))]
		public IDictionary<string, TemplateMapping> Templates { get; internal set; }

		[JsonProperty("cluster_uuid")]
		public string ClusterUUID { get; internal set; }

		[JsonProperty("indices")]
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter))]
		public Dictionary<string, MetadataIndexState> Indices { get; internal set; }
	}
}