using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Gets follower stats. Returns shard-level stats about the following tasks associated with each shard for the specified indices.
		/// </summary>
		public static FollowIndexStatsResponse FollowIndexStats(this IElasticClient client,Indices indices, Func<FollowIndexStatsDescriptor, IFollowIndexStatsRequest> selector = null);

		/// <inheritdoc cref="FollowIndexStats(Indices, System.Func{Nest.FollowIndexStatsDescriptor,Nest.IFollowIndexStatsRequest})" />
		public static FollowIndexStatsResponse FollowIndexStats(this IElasticClient client,IFollowIndexStatsRequest request);

		/// <inheritdoc cref="FollowIndexStats(Indices, System.Func{Nest.FollowIndexStatsDescriptor,Nest.IFollowIndexStatsRequest})" />
		public static Task<FollowIndexStatsResponse> FollowIndexStatsAsync(this IElasticClient client,Indices indices, Func<FollowIndexStatsDescriptor, IFollowIndexStatsRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc cref="FollowIndexStats(Indices, System.Func{Nest.FollowIndexStatsDescriptor,Nest.IFollowIndexStatsRequest})" />
		public static Task<FollowIndexStatsResponse> FollowIndexStatsAsync(this IElasticClient client,IFollowIndexStatsRequest request, CancellationToken ct = default);
	}

}
