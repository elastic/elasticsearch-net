using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Watcher.Stats(), please update this usage.")]
		public static WatcherStatsResponse WatcherStats(this IElasticClient client, Func<WatcherStatsDescriptor, IWatcherStatsRequest> selector = null
		)
			=> client.Watcher.Stats(selector);

		[Obsolete("Moved to client.Watcher.Stats(), please update this usage.")]
		public static WatcherStatsResponse WatcherStats(this IElasticClient client, IWatcherStatsRequest request)
			=> client.Watcher.Stats(request);

		[Obsolete("Moved to client.Watcher.StatsAsync(), please update this usage.")]
		public static Task<WatcherStatsResponse> WatcherStatsAsync(this IElasticClient client,
			Func<WatcherStatsDescriptor, IWatcherStatsRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Watcher.StatsAsync(selector, ct);

		[Obsolete("Moved to client.Watcher.StatsAsync(), please update this usage.")]
		public static Task<WatcherStatsResponse> WatcherStatsAsync(this IElasticClient client, IWatcherStatsRequest request,
			CancellationToken ct = default
		)
			=> client.Watcher.StatsAsync(request, ct);
	}
}
