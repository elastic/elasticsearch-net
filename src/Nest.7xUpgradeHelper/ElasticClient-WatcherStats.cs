using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Gets the current Watcher metrics
		/// </summary>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static WatcherStatsResponse WatcherStats(this IElasticClient client, Func<WatcherStatsDescriptor, IWatcherStatsRequest> selector = null
		)
			=> client.Watcher.Stats(selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static WatcherStatsResponse WatcherStats(this IElasticClient client, IWatcherStatsRequest request)
			=> client.Watcher.Stats(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<WatcherStatsResponse> WatcherStatsAsync(this IElasticClient client,
			Func<WatcherStatsDescriptor, IWatcherStatsRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Watcher.StatsAsync(selector, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<WatcherStatsResponse> WatcherStatsAsync(this IElasticClient client, IWatcherStatsRequest request,
			CancellationToken ct = default
		)
			=> client.Watcher.StatsAsync(request, ct);
	}
}
