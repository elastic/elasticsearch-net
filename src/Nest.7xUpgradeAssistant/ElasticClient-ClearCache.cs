using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Indices.ClearCache(), please update this usage.")]
		public static ClearCacheResponse ClearCache(this IElasticClient client, Indices indices,
			Func<ClearCacheDescriptor, IClearCacheRequest> selector = null
		)
			=> client.Indices.ClearCache(indices, selector);

		[Obsolete("Moved to client.Indices.ClearCache(), please update this usage.")]
		public static ClearCacheResponse ClearCache(this IElasticClient client, IClearCacheRequest request)
			=> client.Indices.ClearCache(request);

		[Obsolete("Moved to client.Indices.ClearCacheAsync(), please update this usage.")]
		public static Task<ClearCacheResponse> ClearCacheAsync(this IElasticClient client,
			Indices indices,
			Func<ClearCacheDescriptor, IClearCacheRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Indices.ClearCacheAsync(indices, selector, ct);

		[Obsolete("Moved to client.Indices.ClearCacheAsync(), please update this usage.")]
		public static Task<ClearCacheResponse> ClearCacheAsync(this IElasticClient client, IClearCacheRequest request, CancellationToken ct = default)
			=> client.Indices.ClearCacheAsync(request, ct);
	}
}
