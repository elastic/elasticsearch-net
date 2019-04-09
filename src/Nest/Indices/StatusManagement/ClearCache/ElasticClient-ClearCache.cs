using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// The clear cache API allows to clear either all caches or specific cached associated with one ore more indices.
		/// <para> </para>
		/// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-clearcache.html
		/// </summary>
		/// <param name="selector">A descriptor that describes the parameters for the clear cache operation</param>
		IClearCacheResponse ClearCache(Indices indices, Func<ClearCacheDescriptor, IClearCacheRequest> selector = null);

		/// <inheritdoc />
		IClearCacheResponse ClearCache(IClearCacheRequest request);

		/// <inheritdoc />
		Task<IClearCacheResponse> ClearCacheAsync(
			Indices indices,
			Func<ClearCacheDescriptor, IClearCacheRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<IClearCacheResponse> ClearCacheAsync(IClearCacheRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IClearCacheResponse ClearCache(Indices indices, Func<ClearCacheDescriptor, IClearCacheRequest> selector = null) =>
			ClearCache(selector.InvokeOrDefault(new ClearCacheDescriptor().Index(indices)));

		/// <inheritdoc />
		public IClearCacheResponse ClearCache(IClearCacheRequest request) =>
			Dispatch2<IClearCacheRequest, ClearCacheResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IClearCacheResponse> ClearCacheAsync(
			Indices indices,
			Func<ClearCacheDescriptor, IClearCacheRequest> selector = null,
			CancellationToken ct = default
		) => ClearCacheAsync(selector.InvokeOrDefault(new ClearCacheDescriptor().Index(indices)), ct);

		/// <inheritdoc />
		public Task<IClearCacheResponse> ClearCacheAsync(IClearCacheRequest request, CancellationToken ct = default) =>
			Dispatch2Async<IClearCacheRequest, IClearCacheResponse, ClearCacheResponse>(request, request.RequestParameters, ct);
	}
}
