using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public interface ISearchShardsResponse : IResponse
	{
		[DataMember(Name ="nodes")]
		IReadOnlyDictionary<string, SearchNode> Nodes { get; }

		[DataMember(Name ="shards")]
		IReadOnlyCollection<IReadOnlyCollection<SearchShard>> Shards { get; }
	}

	public class SearchShardsResponse : ResponseBase, ISearchShardsResponse
	{
		public IReadOnlyDictionary<string, SearchNode> Nodes { get; internal set; } = EmptyReadOnly<string, SearchNode>.Dictionary;

		public IReadOnlyCollection<IReadOnlyCollection<SearchShard>> Shards { get; internal set; } =
			EmptyReadOnly<IReadOnlyCollection<SearchShard>>.Collection;
	}

	[DataContract]
	public class SearchNode
	{
		[DataMember(Name ="name")]
		public string Name { get; internal set; }

		[DataMember(Name ="transport_address")]
		public string TransportAddress { get; internal set; }
	}

	[DataContract]
	public class SearchShard
	{
		[DataMember(Name ="index")]
		public string Index { get; internal set; }

		[DataMember(Name ="node")]
		public string Node { get; internal set; }

		[DataMember(Name ="primary")]
		public bool Primary { get; internal set; }

		[DataMember(Name ="relocating_node")]
		public string RelocatingNode { get; internal set; }

		[DataMember(Name ="shard")]
		public int Shard { get; internal set; }

		[DataMember(Name ="state")]
		public string State { get; internal set; }
	}
}
