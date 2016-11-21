using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public class IndexRoutingTable
	{
		[JsonProperty("shards")]
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter<string, List<RoutingShard>>))]
		public Dictionary<string, List<RoutingShard>> Shards { get; internal set; }
	}
}
