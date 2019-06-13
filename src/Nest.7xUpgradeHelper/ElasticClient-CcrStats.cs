using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Gets cross-cluster replication stats. This API will return all stats related to cross-cluster replication.
		/// In particular, this API returns stats about auto-following, and returns the same shard-level stats as in the get
		/// follower stats API. <see cref="IElasticClient.FollowIndexStats(Nest.Indices,System.Func{Nest.FollowIndexStatsDescriptor,Nest.IFollowIndexStatsRequest})"/>
		/// </summary>
		public static CcrStatsResponse CcrStats(this IElasticClient client,Func<CcrStatsDescriptor, ICcrStatsRequest> selector = null);

		/// <inheritdoc cref="CcrStats(System.Func{Nest.CcrStatsDescriptor,Nest.ICcrStatsRequest})" />
		public static CcrStatsResponse CcrStats(this IElasticClient client,ICcrStatsRequest request);

		/// <inheritdoc cref="CcrStats(System.Func{Nest.CcrStatsDescriptor,Nest.ICcrStatsRequest})" />
		public static Task<CcrStatsResponse> CcrStatsAsync(this IElasticClient client,Func<CcrStatsDescriptor, ICcrStatsRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc cref="CcrStats(System.Func{Nest.CcrStatsDescriptor,Nest.ICcrStatsRequest})" />
		public static Task<CcrStatsResponse> CcrStatsAsync(this IElasticClient client,ICcrStatsRequest request, CancellationToken ct = default);
	}

}
