using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

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
		public static IndicesStatsResponse IndicesStats(this IElasticClient client,Indices indices, Func<IndicesStatsDescriptor, IIndicesStatsRequest> selector = null);

		/// <inheritdoc />
		public static IndicesStatsResponse IndicesStats(this IElasticClient client,IIndicesStatsRequest request);

		/// <inheritdoc />
		public static Task<IndicesStatsResponse> IndicesStatsAsync(this IElasticClient client,
			Indices indices,
			Func<IndicesStatsDescriptor, IIndicesStatsRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<IndicesStatsResponse> IndicesStatsAsync(this IElasticClient client,IIndicesStatsRequest request, CancellationToken ct = default);
	}

}
