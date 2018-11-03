using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public class NodeState
	{
		[JsonProperty("attributes")]
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter<string, string>))]
		public Dictionary<string, string> Attributes { get; internal set; }

		[JsonProperty("name")]
		public string Name { get; internal set; }

		[JsonProperty("transport_address")]
		public string TransportAddress { get; internal set; }
	}
}
