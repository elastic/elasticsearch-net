// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿namespace Nest
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
