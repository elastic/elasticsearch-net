using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public class IndexRoutingTable
	{
		[JsonProperty("shards")]
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter))]
		public Dictionary<string, List<RoutingShard>> Shards { get; internal set; }
	}
}