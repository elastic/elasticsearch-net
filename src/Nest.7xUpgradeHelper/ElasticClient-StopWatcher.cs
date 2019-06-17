using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Watcher.Stop(), please update this usage.")]
		public static StopWatcherResponse StopWatcher(this IElasticClient client, Func<StopWatcherDescriptor, IStopWatcherRequest> selector = null)
			=> client.Watcher.Stop(selector);

		[Obsolete("Moved to client.Watcher.Stop(), please update this usage.")]
		public static StopWatcherResponse StopWatcher(this IElasticClient client, IStopWatcherRequest request)
			=> client.Watcher.Stop(request);

		[Obsolete("Moved to client.Watcher.StopAsync(), please update this usage.")]
		public static Task<StopWatcherResponse> StopWatcherAsync(this IElasticClient client,
			Func<StopWatcherDescriptor, IStopWatcherRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Watcher.StopAsync(selector, ct);

		[Obsolete("Moved to client.Watcher.StopAsync(), please update this usage.")]
		public static Task<StopWatcherResponse> StopWatcherAsync(this IElasticClient client, IStopWatcherRequest request,
			CancellationToken ct = default
		)
			=> client.Watcher.StopAsync(request, ct);
	}
}
