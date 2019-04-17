using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Indices level stats provide statistics on different operations happening on an index. The API provides statistics on
		/// the index level scope (though most stats can also be retrieved using node level scope).
		/// <para> </para>
		/// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-stats.html
		/// </summary>
		/// <param name="selector">Optionaly further describe the indices stats operation</param>
		IndicesStatsResponse IndicesStats(Indices indices, Func<IndicesStatsDescriptor, IIndicesStatsRequest> selector = null);

		/// <inheritdoc />
		IndicesStatsResponse IndicesStats(IIndicesStatsRequest request);

		/// <inheritdoc />
		Task<IndicesStatsResponse> IndicesStatsAsync(
			Indices indices,
			Func<IndicesStatsDescriptor, IIndicesStatsRequest> selector = null,
			CancellationToken cancellationToken = default
		);

		/// <inheritdoc />
		Task<IndicesStatsResponse> IndicesStatsAsync(IIndicesStatsRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IndicesStatsResponse IndicesStats(Indices indices, Func<IndicesStatsDescriptor, IIndicesStatsRequest> selector = null) =>
			IndicesStats(selector.InvokeOrDefault(new IndicesStatsDescriptor().Index(indices)));

		/// <inheritdoc />
		public IndicesStatsResponse IndicesStats(IIndicesStatsRequest request) =>
			DoRequest<IIndicesStatsRequest, IndicesStatsResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IndicesStatsResponse> IndicesStatsAsync(
			Indices indices,
			Func<IndicesStatsDescriptor, IIndicesStatsRequest> selector = null,
			CancellationToken cancellationToken = default
		) => IndicesStatsAsync(selector.InvokeOrDefault(new IndicesStatsDescriptor().Index(indices)), cancellationToken);

		/// <inheritdoc />
		public Task<IndicesStatsResponse> IndicesStatsAsync(IIndicesStatsRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IIndicesStatsRequest, IndicesStatsResponse>(request, request.RequestParameters, ct);
	}
}
