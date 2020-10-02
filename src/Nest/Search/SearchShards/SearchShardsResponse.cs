// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	public class SearchShardsResponse : ResponseBase
	{
		[DataMember(Name = "nodes")]
		public IReadOnlyDictionary<string, SearchNode> Nodes { get; internal set; } = EmptyReadOnly<string, SearchNode>.Dictionary;

		[DataMember(Name = "shards")]
		public IReadOnlyCollection<IReadOnlyCollection<SearchShard>> Shards { get; internal set; } =
			EmptyReadOnly<IReadOnlyCollection<SearchShard>>.Collection;
	}

	[DataContract]
	public class SearchNode
	{
		[DataMember(Name = "name")]
		public string Name { get; internal set; }

		[DataMember(Name = "transport_address")]
		public string TransportAddress { get; internal set; }
	}

	[DataContract]
	public class SearchShard
	{
		[DataMember(Name = "index")]
		public string Index { get; internal set; }

		[DataMember(Name = "node")]
		public string Node { get; internal set; }

		[DataMember(Name = "primary")]
		public bool Primary { get; internal set; }

		[DataMember(Name = "relocating_node")]
		public string RelocatingNode { get; internal set; }

		[DataMember(Name = "shard")]
		public int Shard { get; internal set; }

		[DataMember(Name = "state")]
		public string State { get; internal set; }
	}
}
