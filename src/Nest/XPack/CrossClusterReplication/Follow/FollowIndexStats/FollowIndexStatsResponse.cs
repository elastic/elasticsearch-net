// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	public class FollowIndexStatsResponse : ResponseBase
	{
		/// <inheritdoc cref="FollowIndexStatsResponse.Indices" />
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
