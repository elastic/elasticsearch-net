using System;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest_5_2_0
{
	public class NodeState
	{
		[JsonProperty("name")]
		public string Name { get; internal set; }

		[JsonProperty("transport_address")]
		public string TransportAddress { get; internal set; }

		[JsonProperty("attributes")]
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter<string, string>))]
		public Dictionary<string, string> Attributes { get; internal set; }
	}
}
