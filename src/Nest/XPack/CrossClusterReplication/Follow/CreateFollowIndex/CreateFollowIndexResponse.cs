// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;

namespace Nest
{
	public class CreateFollowIndexResponse : ResponseBase
	{
		[DataMember(Name = "follow_index_created")]
		public bool FollowIndexCreated { get; internal set; }
		[DataMember(Name = "follow_index_shards_acked")]
		public bool FollowIndexShardsAcked { get; internal set; }
		[DataMember(Name = "index_following_started")]
		public bool IndexFollowingStarted { get; internal set; }
	}
}
