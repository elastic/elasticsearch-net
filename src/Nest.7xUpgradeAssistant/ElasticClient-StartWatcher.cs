using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Watcher.Start(), please update this usage.")]
		public static StartWatcherResponse StartWatcher(this IElasticClient client, Func<StartWatcherDescriptor, IStartWatcherRequest> selector = null
		)
			=> client.Watcher.Start(selector);

		[Obsolete("Moved to client.Watcher.Start(), please update this usage.")]
		public static StartWatcherResponse StartWatcher(this IElasticClient client, IStartWatcherRequest request)
			=> client.Watcher.Start(request);

		[Obsolete("Moved to client.Watcher.StartAsync(), please update this usage.")]
		public static Task<StartWatcherResponse> StartWatcherAsync(this IElasticClient client,
			Func<StartWatcherDescriptor, IStartWatcherRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Watcher.StartAsync(selector, ct);

		[Obsolete("Moved to client.Watcher.StartAsync(), please update this usage.")]
		public static Task<StartWatcherResponse> StartWatcherAsync(this IElasticClient client, IStartWatcherRequest request,
			CancellationToken ct = default
		)
			=> client.Watcher.StartAsync(request, ct);
	}
}
