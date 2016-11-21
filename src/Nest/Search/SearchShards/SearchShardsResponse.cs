using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public interface ISearchShardsResponse : IResponse
	{
		[JsonProperty("shards")]
		IReadOnlyCollection<IReadOnlyCollection<SearchShard>> Shards { get; }

		[JsonProperty("nodes")]
		IReadOnlyDictionary<string, SearchNode> Nodes { get; }
	}

	public class SearchShardsResponse : ResponseBase, ISearchShardsResponse
	{
		public IReadOnlyCollection<IReadOnlyCollection<SearchShard>> Shards { get; internal set; } = EmptyReadOnly<IReadOnlyCollection<SearchShard>>.Collection;

		public IReadOnlyDictionary<string, SearchNode> Nodes { get; internal set; } = EmptyReadOnly<string, SearchNode>.Dictionary;
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class SearchNode
	{
		[JsonProperty("name")]
		public string Name { get; internal set; }

		[JsonProperty("transport_address")]
		public string TransportAddress { get; internal set; }
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class SearchShard
	{
		[JsonProperty("state")]
		public string State { get; internal set; }

		[JsonProperty("primary")]
		public bool Primary { get; internal set; }

		[JsonProperty("node")]
		public string Node { get; internal set; }

		[JsonProperty("relocating_node")]
		public string RelocatingNode { get; internal set; }

		[JsonProperty("shard")]
		public int Shard { get; internal set; }

		[JsonProperty("index")]
		public string Index { get; internal set; }
	}
}
