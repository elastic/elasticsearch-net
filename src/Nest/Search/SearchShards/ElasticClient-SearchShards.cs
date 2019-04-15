using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		SearchShardsResponse SearchShards<T>(Func<SearchShardsDescriptor<T>, ISearchShardsRequest> selector) where T : class;

		/// <inheritdoc />
		SearchShardsResponse SearchShards(ISearchShardsRequest request);

		/// <inheritdoc />
		Task<SearchShardsResponse> SearchShardsAsync<T>(Func<SearchShardsDescriptor<T>, ISearchShardsRequest> selector,
			CancellationToken ct = default
		)
			where T : class;

		/// <inheritdoc />
		Task<SearchShardsResponse> SearchShardsAsync(ISearchShardsRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public SearchShardsResponse SearchShards<T>(Func<SearchShardsDescriptor<T>, ISearchShardsRequest> selector)
			where T : class =>
			SearchShards(selector?.Invoke(new SearchShardsDescriptor<T>()));

		/// <inheritdoc />
		public SearchShardsResponse SearchShards(ISearchShardsRequest request) =>
			DoRequest<ISearchShardsRequest, SearchShardsResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<SearchShardsResponse> SearchShardsAsync<T>(
			Func<SearchShardsDescriptor<T>, ISearchShardsRequest> selector,
			CancellationToken ct = default
		)
			where T : class =>
			SearchShardsAsync(selector?.Invoke(new SearchShardsDescriptor<T>()), ct);

		/// <inheritdoc />
		public Task<SearchShardsResponse> SearchShardsAsync(ISearchShardsRequest request, CancellationToken ct = default) =>
			DoRequestAsync<ISearchShardsRequest, SearchShardsResponse, SearchShardsResponse>(request, request.RequestParameters, ct);
	}
}
