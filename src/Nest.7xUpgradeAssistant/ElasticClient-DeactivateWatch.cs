using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Watcher.Deactivate(), please update this usage.")]
		public static DeactivateWatchResponse DeactivateWatch(this IElasticClient client, Id id,
			Func<DeactivateWatchDescriptor, IDeactivateWatchRequest> selector = null
		)
			=> client.Watcher.Deactivate(id, selector);

		[Obsolete("Moved to client.Watcher.Deactivate(), please update this usage.")]
		public static DeactivateWatchResponse DeactivateWatch(this IElasticClient client, IDeactivateWatchRequest request)
			=> client.Watcher.Deactivate(request);

		[Obsolete("Moved to client.Watcher.DeactivateAsync(), please update this usage.")]
		public static Task<DeactivateWatchResponse> DeactivateWatchAsync(this IElasticClient client, Id id,
			Func<DeactivateWatchDescriptor, IDeactivateWatchRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Watcher.DeactivateAsync(id, selector, ct);

		[Obsolete("Moved to client.Watcher.DeactivateAsync(), please update this usage.")]
		public static Task<DeactivateWatchResponse> DeactivateWatchAsync(this IElasticClient client, IDeactivateWatchRequest request,
			CancellationToken ct = default
		)
			=> client.Watcher.DeactivateAsync(request, ct);
	}
}
