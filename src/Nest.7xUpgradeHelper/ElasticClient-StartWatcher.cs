using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Starts the Watcher/Alerting service, if the service is not already running
		/// </summary>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static StartWatcherResponse StartWatcher(this IElasticClient client, Func<StartWatcherDescriptor, IStartWatcherRequest> selector = null
		)
			=> client.Watcher.Start(selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static StartWatcherResponse StartWatcher(this IElasticClient client, IStartWatcherRequest request)
			=> client.Watcher.Start(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<StartWatcherResponse> StartWatcherAsync(this IElasticClient client,
			Func<StartWatcherDescriptor, IStartWatcherRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Watcher.StartAsync(selector, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<StartWatcherResponse> StartWatcherAsync(this IElasticClient client, IStartWatcherRequest request,
			CancellationToken ct = default
		)
			=> client.Watcher.StartAsync(request, ct);
	}
}
