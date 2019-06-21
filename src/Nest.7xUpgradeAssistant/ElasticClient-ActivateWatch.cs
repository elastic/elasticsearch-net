using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Watcher.Activate(), please update this usage.")]
		public static ActivateWatchResponse ActivateWatch(this IElasticClient client, Id id,
			Func<ActivateWatchDescriptor, IActivateWatchRequest> selector = null
		)
			=> client.Watcher.Activate(id, selector);

		[Obsolete("Moved to client.Watcher.Activate(), please update this usage.")]
		public static ActivateWatchResponse ActivateWatch(this IElasticClient client, IActivateWatchRequest request)
			=> client.Watcher.Activate(request);

		[Obsolete("Moved to client.Watcher.ActivateAsync(), please update this usage.")]
		public static Task<ActivateWatchResponse> ActivateWatchAsync(this IElasticClient client, Id id,
			Func<ActivateWatchDescriptor, IActivateWatchRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Watcher.ActivateAsync(id, selector, ct);

		[Obsolete("Moved to client.Watcher.ActivateAsync(), please update this usage.")]
		public static Task<ActivateWatchResponse> ActivateWatchAsync(this IElasticClient client, IActivateWatchRequest request,
			CancellationToken ct = default
		)
			=> client.Watcher.ActivateAsync(request, ct);
	}
}
