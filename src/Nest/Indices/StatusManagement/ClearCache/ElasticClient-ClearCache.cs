using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using System.Threading;

namespace Nest
{

	public partial interface IElasticClient
	{
		/// <summary>
		/// The clear cache API allows to clear either all caches or specific cached associated with one ore more indices.
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-clearcache.html
		/// </summary>
		/// <param name="selector">A descriptor that describes the parameters for the clear cache operation</param>
		IClearCacheResponse ClearCache(Indices indices, Func<ClearCacheDescriptor, IClearCacheRequest> selector = null);

		/// <inheritdoc/>
		IClearCacheResponse ClearCache(IClearCacheRequest request);

		/// <inheritdoc/>
		Task<IClearCacheResponse> ClearCacheAsync(
			Indices indices,
			Func<ClearCacheDescriptor, IClearCacheRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc/>
		Task<IClearCacheResponse> ClearCacheAsync(IClearCacheRequest request, CancellationToken cancellationToken = default(CancellationToken));

	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IClearCacheResponse ClearCache(Indices indices, Func<ClearCacheDescriptor, IClearCacheRequest> selector = null) =>
			this.Dispatcher.Dispatch<IClearCacheRequest, ClearCacheRequestParameters, ClearCacheResponse>(
				selector.InvokeOrDefault(new ClearCacheDescriptor().Index(indices)),
				(p, d) => this.LowLevelDispatch.IndicesClearCacheDispatch<ClearCacheResponse>(p)
			);

		/// <inheritdoc/>
		public IClearCacheResponse ClearCache(IClearCacheRequest request) =>
			this.Dispatcher.Dispatch<IClearCacheRequest, ClearCacheRequestParameters, ClearCacheResponse>(
				request,
				(p, d) => this.LowLevelDispatch.IndicesClearCacheDispatch<ClearCacheResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IClearCacheResponse> ClearCacheAsync(
			Indices indices,
			Func<ClearCacheDescriptor, IClearCacheRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) => this.ClearCacheAsync(selector.InvokeOrDefault(new ClearCacheDescriptor().Index(indices)), cancellationToken);

		/// <inheritdoc/>
		public Task<IClearCacheResponse> ClearCacheAsync(IClearCacheRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<IClearCacheRequest, ClearCacheRequestParameters, ClearCacheResponse, IClearCacheResponse>(
				request,
				cancellationToken,
				(p, d, c) => this.LowLevelDispatch.IndicesClearCacheDispatchAsync<ClearCacheResponse>(p, c)
			);
	}
}
