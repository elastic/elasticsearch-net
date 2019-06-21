using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Cluster.Stats(), please update this usage.")]
		public static ClusterStatsResponse ClusterStats(this IElasticClient client, Func<ClusterStatsDescriptor, IClusterStatsRequest> selector = null
		)
			=> client.Cluster.Stats(selector);

		[Obsolete("Moved to client.Cluster.StatsAsync(), please update this usage.")]
		public static Task<ClusterStatsResponse> ClusterStatsAsync(this IElasticClient client,
			Func<ClusterStatsDescriptor, IClusterStatsRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Cluster.StatsAsync(selector, ct);

		[Obsolete("Moved to client.Cluster.Stats(), please update this usage.")]
		public static ClusterStatsResponse ClusterStats(this IElasticClient client, IClusterStatsRequest request)
			=> client.Cluster.Stats(request);

		[Obsolete("Moved to client.Cluster.StatsAsync(), please update this usage.")]
		public static Task<ClusterStatsResponse> ClusterStatsAsync(this IElasticClient client, IClusterStatsRequest request,
			CancellationToken ct = default
		)
			=> client.Cluster.StatsAsync(request, ct);
	}
}
