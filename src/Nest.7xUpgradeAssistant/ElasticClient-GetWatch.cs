using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Watcher.Get(), please update this usage.")]
		public static GetWatchResponse GetWatch(this IElasticClient client, Id watchId, Func<GetWatchDescriptor, IGetWatchRequest> selector = null)
			=> client.Watcher.Get(watchId, selector);

		[Obsolete("Moved to client.Watcher.Get(), please update this usage.")]
		public static GetWatchResponse GetWatch(this IElasticClient client, IGetWatchRequest request)
			=> client.Watcher.Get(request);

		[Obsolete("Moved to client.Watcher.GetAsync(), please update this usage.")]
		public static Task<GetWatchResponse> GetWatchAsync(this IElasticClient client, Id watchId,
			Func<GetWatchDescriptor, IGetWatchRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Watcher.GetAsync(watchId, selector, ct);

		[Obsolete("Moved to client.Watcher.GetAsync(), please update this usage.")]
		public static Task<GetWatchResponse> GetWatchAsync(this IElasticClient client, IGetWatchRequest request, CancellationToken ct = default)
			=> client.Watcher.GetAsync(request,ct);
	}
}
