using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Indices.Stats(), please update this usage.")]
		public static IndicesStatsResponse IndicesStats(this IElasticClient client, Indices indices,
			Func<IndicesStatsDescriptor, IIndicesStatsRequest> selector = null
		)
			=> client.Indices.Stats(indices, selector);

		[Obsolete("Moved to client.Indices.Stats(), please update this usage.")]
		public static IndicesStatsResponse IndicesStats(this IElasticClient client, IIndicesStatsRequest request)
			=> client.Indices.Stats(request);

		[Obsolete("Moved to client.Indices.StatsAsync(), please update this usage.")]
		public static Task<IndicesStatsResponse> IndicesStatsAsync(this IElasticClient client,
			Indices indices,
			Func<IndicesStatsDescriptor, IIndicesStatsRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Indices.StatsAsync(indices, selector, ct);

		[Obsolete("Moved to client.Indices.StatsAsync(), please update this usage.")]
		public static Task<IndicesStatsResponse> IndicesStatsAsync(this IElasticClient client, IIndicesStatsRequest request,
			CancellationToken ct = default
		)
			=> client.Indices.StatsAsync(request, ct);
	}
}
