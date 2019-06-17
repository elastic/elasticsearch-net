using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Indices level stats provide statistics on different operations happening on an index. The API provides statistics on
		/// the index level scope (though most stats can also be retrieved using node level scope).
		/// <para> </para>
		/// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-stats.html
		/// </summary>
		/// <param name="selector">Optionaly further describe the indices stats operation</param>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static IndicesStatsResponse IndicesStats(this IElasticClient client, Indices indices,
			Func<IndicesStatsDescriptor, IIndicesStatsRequest> selector = null
		)
			=> client.Indices.Stats(indices, selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static IndicesStatsResponse IndicesStats(this IElasticClient client, IIndicesStatsRequest request)
			=> client.Indices.Stats(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<IndicesStatsResponse> IndicesStatsAsync(this IElasticClient client,
			Indices indices,
			Func<IndicesStatsDescriptor, IIndicesStatsRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Indices.StatsAsync(indices, selector, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<IndicesStatsResponse> IndicesStatsAsync(this IElasticClient client, IIndicesStatsRequest request,
			CancellationToken ct = default
		)
			=> client.Indices.StatsAsync(request, ct);
	}
}
