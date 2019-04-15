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
		IndicesShardStoresResponse IndicesShardStores(Func<IndicesShardStoresDescriptor, IIndicesShardStoresRequest> selector = null);

		/// <inheritdoc />
		IndicesShardStoresResponse IndicesShardStores(IIndicesShardStoresRequest request);

		/// <inheritdoc />
		Task<IndicesShardStoresResponse> IndicesShardStoresAsync(
			Func<IndicesShardStoresDescriptor, IIndicesShardStoresRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<IndicesShardStoresResponse> IndicesShardStoresAsync(IIndicesShardStoresRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IndicesShardStoresResponse IndicesShardStores(Func<IndicesShardStoresDescriptor, IIndicesShardStoresRequest> selector = null) =>
			IndicesShardStores(selector.InvokeOrDefault(new IndicesShardStoresDescriptor()));

		/// <inheritdoc />
		public IndicesShardStoresResponse IndicesShardStores(IIndicesShardStoresRequest request) =>
			DoRequest<IIndicesShardStoresRequest, IndicesShardStoresResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IndicesShardStoresResponse> IndicesShardStoresAsync(
			Func<IndicesShardStoresDescriptor, IIndicesShardStoresRequest> selector = null,
			CancellationToken ct = default
		) => IndicesShardStoresAsync(selector.InvokeOrDefault(new IndicesShardStoresDescriptor()), ct);

		/// <inheritdoc />
		public Task<IndicesShardStoresResponse> IndicesShardStoresAsync(IIndicesShardStoresRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IIndicesShardStoresRequest, IndicesShardStoresResponse, IndicesShardStoresResponse>(request, request.RequestParameters, ct);
	}
}
