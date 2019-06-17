using System;
using System.Threading;
using System.Threading.Tasks;

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
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static ClearCacheResponse ClearCache(this IElasticClient client, Indices indices,
			Func<ClearCacheDescriptor, IClearCacheRequest> selector = null
		)
			=> client.Indices.ClearCache(indices, selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static ClearCacheResponse ClearCache(this IElasticClient client, IClearCacheRequest request)
			=> client.Indices.ClearCache(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<ClearCacheResponse> ClearCacheAsync(this IElasticClient client,
			Indices indices,
			Func<ClearCacheDescriptor, IClearCacheRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Indices.ClearCacheAsync(indices, selector, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<ClearCacheResponse> ClearCacheAsync(this IElasticClient client, IClearCacheRequest request, CancellationToken ct = default)
			=> client.Indices.ClearCacheAsync(request, ct);
	}
}
