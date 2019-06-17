using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Gets follower stats. Returns shard-level stats about the following tasks associated with each shard for the specified
		/// indices.
		/// </summary>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static FollowIndexStatsResponse FollowIndexStats(this IElasticClient client, Indices indices,
			Func<FollowIndexStatsDescriptor, IFollowIndexStatsRequest> selector = null
		)
			=> client.CrossClusterReplication.FollowIndexStats(indices, selector);

		/// <inheritdoc
		///     cref="FollowIndexStats(Indices, System.Func{Nest.FollowIndexStatsDescriptor,Nest.IFollowIndexStatsRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static FollowIndexStatsResponse FollowIndexStats(this IElasticClient client, IFollowIndexStatsRequest request)
			=> client.CrossClusterReplication.FollowIndexStats(request);

		/// <inheritdoc
		///     cref="FollowIndexStats(Indices, System.Func{Nest.FollowIndexStatsDescriptor,Nest.IFollowIndexStatsRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<FollowIndexStatsResponse> FollowIndexStatsAsync(this IElasticClient client, Indices indices,
			Func<FollowIndexStatsDescriptor, IFollowIndexStatsRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.CrossClusterReplication.FollowIndexStatsAsync(indices, selector, ct);

		/// <inheritdoc
		///     cref="FollowIndexStats(Indices, System.Func{Nest.FollowIndexStatsDescriptor,Nest.IFollowIndexStatsRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<FollowIndexStatsResponse> FollowIndexStatsAsync(this IElasticClient client, IFollowIndexStatsRequest request,
			CancellationToken ct = default
		)
			=> client.CrossClusterReplication.FollowIndexStatsAsync(request, ct);
	}
}
