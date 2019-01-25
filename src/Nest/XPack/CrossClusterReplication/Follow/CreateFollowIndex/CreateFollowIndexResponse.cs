using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface ICreateFollowIndexResponse : IResponse
	{
		[JsonProperty("follow_index_created")]
		bool FollowIndexCreated { get; }

		[JsonProperty("follow_index_shards_acked")]
		bool FollowIndexShardsAcked { get; }

		[JsonProperty("index_following_started")]
		bool IndexFollowingStarted { get; }
	}

	public class CreateFollowIndexResponse : ResponseBase, ICreateFollowIndexResponse
	{
		public bool FollowIndexCreated { get; internal set; }
		public bool FollowIndexShardsAcked { get; internal set; }
		public bool IndexFollowingStarted { get; internal set; }
	}
}
