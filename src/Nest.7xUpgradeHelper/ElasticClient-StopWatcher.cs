using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Stops the Watcher/Alerting service, if the service is running
		/// </summary>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static StopWatcherResponse StopWatcher(this IElasticClient client, Func<StopWatcherDescriptor, IStopWatcherRequest> selector = null)
			=> client.Watcher.Stop(selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static StopWatcherResponse StopWatcher(this IElasticClient client, IStopWatcherRequest request)
			=> client.Watcher.Stop(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<StopWatcherResponse> StopWatcherAsync(this IElasticClient client,
			Func<StopWatcherDescriptor, IStopWatcherRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Watcher.StopAsync(selector, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<StopWatcherResponse> StopWatcherAsync(this IElasticClient client, IStopWatcherRequest request,
			CancellationToken ct = default
		)
			=> client.Watcher.StopAsync(request, ct);
	}
}
