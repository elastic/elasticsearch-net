namespace Nest
{
	/// <summary>
	/// Gets follower stats. Will return shard-level stats about the following tasks associated with each shard for the
	/// specified indices.
	/// </summary>
	[MapsApi("ccr.follow_stats.json")]
	public partial interface IFollowIndexStatsRequest { }

	/// <inheritdoc cref="IFollowIndexStatsRequest" />
	public partial class FollowIndexStatsRequest { }

	/// <inheritdoc cref="IFollowIndexStatsRequest" />
	public partial class FollowIndexStatsDescriptor { }
}
