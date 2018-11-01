using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		///     Indices level stats provide statistics on different operations happening on an index. The API provides statistics on
		///     the index level scope (though most stats can also be retrieved using node level scope).
		///     <para> </para>
		///     http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-stats.html
		/// </summary>
		/// <param name="selector">Optionaly further describe the indices stats operation</param>
		IIndicesStatsResponse IndicesStats(Indices indices, Func<IndicesStatsDescriptor, IIndicesStatsRequest> selector = null);

		/// <inheritdoc />
		IIndicesStatsResponse IndicesStats(IIndicesStatsRequest request);

		/// <inheritdoc />
		Task<IIndicesStatsResponse> IndicesStatsAsync(
			Indices indices,
			Func<IndicesStatsDescriptor, IIndicesStatsRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<IIndicesStatsResponse> IndicesStatsAsync(IIndicesStatsRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IIndicesStatsResponse IndicesStats(Indices indices, Func<IndicesStatsDescriptor, IIndicesStatsRequest> selector = null) =>
			IndicesStats(selector.InvokeOrDefault(new IndicesStatsDescriptor().Index(indices)));

		/// <inheritdoc />
		public IIndicesStatsResponse IndicesStats(IIndicesStatsRequest request) =>
			Dispatcher.Dispatch<IIndicesStatsRequest, IndicesStatsRequestParameters, IndicesStatsResponse>(
				request,
				(p, d) => LowLevelDispatch.IndicesStatsDispatch<IndicesStatsResponse>(p)
			);

		/// <inheritdoc />
		public Task<IIndicesStatsResponse> IndicesStatsAsync(
			Indices indices,
			Func<IndicesStatsDescriptor, IIndicesStatsRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) => IndicesStatsAsync(selector.InvokeOrDefault(new IndicesStatsDescriptor().Index(indices)), cancellationToken);

		/// <inheritdoc />
		public Task<IIndicesStatsResponse> IndicesStatsAsync(IIndicesStatsRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			Dispatcher.DispatchAsync<IIndicesStatsRequest, IndicesStatsRequestParameters, IndicesStatsResponse, IIndicesStatsResponse>(
				request,
				cancellationToken,
				(p, d, c) => LowLevelDispatch.IndicesStatsDispatchAsync<IndicesStatsResponse>(p, c)
			);
	}
}
