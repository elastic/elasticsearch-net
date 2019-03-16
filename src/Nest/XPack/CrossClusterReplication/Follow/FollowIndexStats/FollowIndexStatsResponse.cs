using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	public interface IFollowIndexStatsResponse : IResponse
	{
		[DataMember(Name = "indices")]
		IReadOnlyCollection<FollowIndexStats> Indices { get; }
	}

	public class FollowIndexStatsResponse : ResponseBase, IFollowIndexStatsResponse
	{
		/// <inheritdoc cref="IFollowIndexStatsResponse.Indices" />
		[DataMember(Name = "indices")]
		public IReadOnlyCollection<FollowIndexStats> Indices { get; internal set; } = EmptyReadOnly<FollowIndexStats>.Collection;
	}

	public class FollowIndexStats
	{
		[DataMember(Name = "index")]
		public string Index { get; internal set; }

		[DataMember(Name = "shards")]
		public IReadOnlyCollection<FollowIndexShardStats> Shards { get; internal set; } = EmptyReadOnly<FollowIndexShardStats>.Collection;
	}
}
