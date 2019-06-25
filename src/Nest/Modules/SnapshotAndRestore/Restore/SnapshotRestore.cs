using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	public class SnapshotRestore
	{
		[DataMember(Name ="indices")]
		public IReadOnlyCollection<IndexName> Indices { get; internal set; } =
			EmptyReadOnly<IndexName>.Collection;

		[DataMember(Name ="snapshot")]
		public string Name { get; internal set; }

		[DataMember(Name ="shards")]
		public ShardStatistics Shards { get; internal set; }
	}
}
