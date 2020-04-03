using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[DataContract]
	public class SnapshotShardFailure
	{
		[DataMember(Name ="index")]
		public string Index { get; set; }

		[DataMember(Name ="node_id")]
		public string NodeId { get; set; }

		[DataMember(Name ="reason")]
		public string Reason { get; set; }

		[DataMember(Name ="shard_id")]
		[JsonFormatter(typeof(IntStringFormatter))]
		public string ShardId { get; set; }

		[DataMember(Name ="status")]
		public string Status { get; set; }
	}
}
