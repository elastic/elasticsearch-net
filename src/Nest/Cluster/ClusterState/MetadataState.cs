using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public class MetadataState
	{
		[JsonProperty("cluster_uuid")]
		public string ClusterUUID { get; internal set; }

		[JsonProperty("indices")]
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter<string, MetadataIndexState>))]
		public IReadOnlyDictionary<string, MetadataIndexState> Indices { get; internal set; }

		[JsonProperty("templates")]
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter<string, TemplateMapping>))]
		public IReadOnlyDictionary<string, TemplateMapping> Templates { get; internal set; }

		[JsonProperty("stored_scripts")]
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter<string, StoredScriptMapping>))]
		public IReadOnlyDictionary<string, StoredScriptMapping> StoredScripts { get; internal set; }
	}
}
