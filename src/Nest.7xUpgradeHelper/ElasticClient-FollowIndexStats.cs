using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.CrossClusterReplication.FollowIndexStats(), please update this usage.")]
		public static FollowIndexStatsResponse FollowIndexStats(this IElasticClient client, Indices indices,
			Func<FollowIndexStatsDescriptor, IFollowIndexStatsRequest> selector = null
		)
			=> client.CrossClusterReplication.FollowIndexStats(indices, selector);

		[Obsolete("Moved to client.CrossClusterReplication.FollowIndexStats(), please update this usage.")]
		public static FollowIndexStatsResponse FollowIndexStats(this IElasticClient client, IFollowIndexStatsRequest request)
			=> client.CrossClusterReplication.FollowIndexStats(request);

		[Obsolete("Moved to client.CrossClusterReplication.FollowIndexStatsAsync(), please update this usage.")]
		public static Task<FollowIndexStatsResponse> FollowIndexStatsAsync(this IElasticClient client, Indices indices,
			Func<FollowIndexStatsDescriptor, IFollowIndexStatsRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.CrossClusterReplication.FollowIndexStatsAsync(indices, selector, ct);

		[Obsolete("Moved to client.CrossClusterReplication.FollowIndexStatsAsync(), please update this usage.")]
		public static Task<FollowIndexStatsResponse> FollowIndexStatsAsync(this IElasticClient client, IFollowIndexStatsRequest request,
			CancellationToken ct = default
		)
			=> client.CrossClusterReplication.FollowIndexStatsAsync(request, ct);
	}
}
