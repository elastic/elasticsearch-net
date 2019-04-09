using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		ISearchShardsResponse SearchShards<T>(Func<SearchShardsDescriptor<T>, ISearchShardsRequest> selector) where T : class;

		/// <inheritdoc />
		ISearchShardsResponse SearchShards(ISearchShardsRequest request);

		/// <inheritdoc />
		Task<ISearchShardsResponse> SearchShardsAsync<T>(Func<SearchShardsDescriptor<T>, ISearchShardsRequest> selector,
			CancellationToken ct = default
		)
			where T : class;

		/// <inheritdoc />
		Task<ISearchShardsResponse> SearchShardsAsync(ISearchShardsRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public ISearchShardsResponse SearchShards<T>(Func<SearchShardsDescriptor<T>, ISearchShardsRequest> selector)
			where T : class =>
			SearchShards(selector?.Invoke(new SearchShardsDescriptor<T>()));

		/// <inheritdoc />
		public ISearchShardsResponse SearchShards(ISearchShardsRequest request) =>
			Dispatch2<ISearchShardsRequest, SearchShardsResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<ISearchShardsResponse> SearchShardsAsync<T>(
			Func<SearchShardsDescriptor<T>, ISearchShardsRequest> selector,
			CancellationToken ct = default
		)
			where T : class =>
			SearchShardsAsync(selector?.Invoke(new SearchShardsDescriptor<T>()), ct);

		/// <inheritdoc />
		public Task<ISearchShardsResponse> SearchShardsAsync(ISearchShardsRequest request, CancellationToken ct = default) =>
			Dispatch2Async<ISearchShardsRequest, ISearchShardsResponse, SearchShardsResponse>(request, request.RequestParameters, ct);
	}
}
