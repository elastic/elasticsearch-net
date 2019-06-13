using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// The clear cache API allows to clear either all caches or specific cached associated with one ore more indices.
		/// <para> </para>
		/// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/indices-clearcache.html
		/// </summary>
		/// <param name="selector">A descriptor that describes the parameters for the clear cache operation</param>
		public static ClearCacheResponse ClearCache(this IElasticClient client,Indices indices, Func<ClearCacheDescriptor, IClearCacheRequest> selector = null);

		/// <inheritdoc />
		public static ClearCacheResponse ClearCache(this IElasticClient client,IClearCacheRequest request);

		/// <inheritdoc />
		public static Task<ClearCacheResponse> ClearCacheAsync(this IElasticClient client,
			Indices indices,
			Func<ClearCacheDescriptor, IClearCacheRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<ClearCacheResponse> ClearCacheAsync(this IElasticClient client,IClearCacheRequest request, CancellationToken ct = default);
	}

}
