using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IFollowIndexStatsResponse : IResponse
	{
		[JsonProperty("indices")]
		IReadOnlyCollection<FollowIndexStats> Indices { get; }
	}

	public class FollowIndexStatsResponse : ResponseBase, IFollowIndexStatsResponse
	{
		/// <inheritdoc cref="IFollowIndexStatsResponse.Indices" />
		[JsonProperty("indices")]
		public IReadOnlyCollection<FollowIndexStats> Indices { get; internal set; } = EmptyReadOnly<FollowIndexStats>.Collection;
	}

	public class FollowIndexStats
	{
		[JsonProperty("index")]
		public string Index { get; internal set; }

		[JsonProperty("shards")]
		public IReadOnlyCollection<FollowIndexShardStats> Shards { get; internal set; } = EmptyReadOnly<FollowIndexShardStats>.Collection;
	}
}
