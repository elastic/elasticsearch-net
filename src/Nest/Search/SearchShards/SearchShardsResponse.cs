using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public interface ISearchShardsResponse : IResponse
	{
		[JsonProperty("shards")]
		IEnumerable<IEnumerable<SearchShard>> Shards { get; }

		[JsonProperty("nodes")]
		IDictionary<string, SearchNode> Nodes { get; }
	}

	public class SearchShardsResponse : ResponseBase, ISearchShardsResponse
	{
		public IEnumerable<IEnumerable<SearchShard>> Shards { get; internal set; }

		public IDictionary<string, SearchNode> Nodes { get; internal set; }
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class SearchNode
	{
		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("transport_address")]
		public string TransportAddress { get; set; }
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class SearchShard
	{
		[JsonProperty("state")]
		public string State { get; set; }

		[JsonProperty("primary")]
		public bool Primary { get; set; }

		[JsonProperty("node")]
		public string Node { get; set; }

		[JsonProperty("relocating_node")]
		public string RelocatingNode { get; set; }

		[JsonProperty("shard")]
		public int Shard { get; set; }

		[JsonProperty("index")]
		public string Index { get; set; }
	}
}