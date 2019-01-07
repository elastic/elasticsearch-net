using System;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// This API gets follower stats. This API will return shard-level stats about the following tasks
	/// associated with each shard for the specified indices.
	/// </summary>
	[MapsApi("ccr.follow_stats.json")]
	[ContractJsonConverter(typeof(ReadAsTypeJsonConverter<FollowIndexStatsRequest>))]
	public partial interface IFollowIndexStatsRequest { }

	/// <inheritdoc cref="IFollowIndexStatsRequest"/>
	public partial class FollowIndexStatsRequest { }

	/// <inheritdoc cref="IFollowIndexStatsRequest"/>
	public partial class FollowIndexStatsDescriptor { }
}
