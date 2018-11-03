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
			CancellationToken cancellationToken = default(CancellationToken)
		)
			where T : class;

		/// <inheritdoc />
		Task<ISearchShardsResponse> SearchShardsAsync(ISearchShardsRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public ISearchShardsResponse SearchShards<T>(Func<SearchShardsDescriptor<T>, ISearchShardsRequest> selector) where T : class =>
			SearchShards(selector?.Invoke(new SearchShardsDescriptor<T>()));

		/// <inheritdoc />
		public ISearchShardsResponse SearchShards(ISearchShardsRequest request) =>
			Dispatcher.Dispatch<ISearchShardsRequest, SearchShardsRequestParameters, SearchShardsResponse>(
				request,
				(p, d) => LowLevelDispatch.SearchShardsDispatch<SearchShardsResponse>(p)
			);

		/// <inheritdoc />
		public Task<ISearchShardsResponse> SearchShardsAsync<T>(Func<SearchShardsDescriptor<T>, ISearchShardsRequest> selector,
			CancellationToken cancellationToken = default(CancellationToken)
		)
			where T : class =>
			SearchShardsAsync(selector?.Invoke(new SearchShardsDescriptor<T>()), cancellationToken);

		/// <inheritdoc />
		public Task<ISearchShardsResponse> SearchShardsAsync(ISearchShardsRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			Dispatcher.DispatchAsync<ISearchShardsRequest, SearchShardsRequestParameters, SearchShardsResponse, ISearchShardsResponse>(
				request,
				cancellationToken,
				(p, d, c) => LowLevelDispatch.SearchShardsDispatchAsync<SearchShardsResponse>(p, c)
			);
	}
}
