using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	public class FollowInfoResponse : ResponseBase
	{
		/// <inheritdoc cref="FollowInfoResponse.FollowerIndices" />
		[DataMember(Name = "follower_indices")]
		public IReadOnlyCollection<FollowerInfo> FollowerIndices { get; internal set; } = EmptyReadOnly<FollowerInfo>.Collection;
	}

	public class FollowerInfo
	{
		[DataMember(Name = "follower_index")]
		public string FollowerIndex { get; internal set; }
	}
}
