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
		ClearCacheResponse ClearCache(Indices indices, Func<ClearCacheDescriptor, IClearCacheRequest> selector = null);

		/// <inheritdoc />
		ClearCacheResponse ClearCache(IClearCacheRequest request);

		/// <inheritdoc />
		Task<ClearCacheResponse> ClearCacheAsync(
			Indices indices,
			Func<ClearCacheDescriptor, IClearCacheRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<ClearCacheResponse> ClearCacheAsync(IClearCacheRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public ClearCacheResponse ClearCache(Indices indices, Func<ClearCacheDescriptor, IClearCacheRequest> selector = null) =>
			ClearCache(selector.InvokeOrDefault(new ClearCacheDescriptor().Index(indices)));

		/// <inheritdoc />
		public ClearCacheResponse ClearCache(IClearCacheRequest request) =>
			DoRequest<IClearCacheRequest, ClearCacheResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<ClearCacheResponse> ClearCacheAsync(
			Indices indices,
			Func<ClearCacheDescriptor, IClearCacheRequest> selector = null,
			CancellationToken ct = default
		) => ClearCacheAsync(selector.InvokeOrDefault(new ClearCacheDescriptor().Index(indices)), ct);

		/// <inheritdoc />
		public Task<ClearCacheResponse> ClearCacheAsync(IClearCacheRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IClearCacheRequest, ClearCacheResponse, ClearCacheResponse>(request, request.RequestParameters, ct);
	}
}
