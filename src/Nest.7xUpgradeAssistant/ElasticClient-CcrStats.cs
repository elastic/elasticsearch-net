using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.CrossClusterReplication.Stats(), please update this usage.")]
		public static CcrStatsResponse CcrStats(this IElasticClient client, Func<CcrStatsDescriptor, ICcrStatsRequest> selector = null)
			=> client.CrossClusterReplication.Stats(selector);

		[Obsolete("Moved to client.CrossClusterReplication.Stats(), please update this usage.")]
		public static CcrStatsResponse CcrStats(this IElasticClient client, ICcrStatsRequest request)
			=> client.CrossClusterReplication.Stats(request);

		[Obsolete("Moved to client.CrossClusterReplication.StatsAsync(), please update this usage.")]
		public static Task<CcrStatsResponse> CcrStatsAsync(this IElasticClient client, Func<CcrStatsDescriptor, ICcrStatsRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.CrossClusterReplication.StatsAsync(selector, ct);

		[Obsolete("Moved to client.CrossClusterReplication.StatsAsync(), please update this usage.")]
		public static Task<CcrStatsResponse> CcrStatsAsync(this IElasticClient client, ICcrStatsRequest request, CancellationToken ct = default)
			=> client.CrossClusterReplication.StatsAsync(request, ct);
	}
}
