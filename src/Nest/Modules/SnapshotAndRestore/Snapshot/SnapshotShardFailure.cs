// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;

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
		public int ShardId { get; set; }

		[DataMember(Name ="status")]
		public string Status { get; set; }
	}
}
