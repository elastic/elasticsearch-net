using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Watcher.Put(), please update this usage.")]
		public static PutWatchResponse PutWatch(this IElasticClient client, Id watchId, Func<PutWatchDescriptor, IPutWatchRequest> selector = null)
			=> client.Watcher.Put(watchId, selector);

		[Obsolete("Moved to client.Watcher.Put(), please update this usage.")]
		public static PutWatchResponse PutWatch(this IElasticClient client, IPutWatchRequest request)
			=> client.Watcher.Put(request);

		[Obsolete("Moved to client.Watcher.PutAsync(), please update this usage.")]
		public static Task<PutWatchResponse> PutWatchAsync(this IElasticClient client, Id watchId,
			Func<PutWatchDescriptor, IPutWatchRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Watcher.PutAsync(watchId, selector, ct);

		[Obsolete("Moved to client.Watcher.PutAsync(), please update this usage.")]
		public static Task<PutWatchResponse> PutWatchAsync(this IElasticClient client, IPutWatchRequest request, CancellationToken ct = default)
			=> client.Watcher.PutAsync(request, ct);
	}
}
