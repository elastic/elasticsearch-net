using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	public interface ICreateFollowIndexResponse : IResponse
	{
		[DataMember(Name = "follow_index_created")]
		bool FollowIndexCreated { get; }

		[DataMember(Name = "follow_index_shards_acked")]
		bool FollowIndexShardsAcked { get; }

		[DataMember(Name = "index_following_started")]
		bool IndexFollowingStarted { get; }
	}

	public class CreateFollowIndexResponse : ResponseBase, ICreateFollowIndexResponse
	{
		public bool FollowIndexCreated { get; internal set; }
		public bool FollowIndexShardsAcked { get; internal set; }
		public bool IndexFollowingStarted { get; internal set; }
	}
}
