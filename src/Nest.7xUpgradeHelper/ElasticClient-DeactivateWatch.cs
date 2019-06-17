using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Deactivates a currently active watch.
		/// </summary>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static DeactivateWatchResponse DeactivateWatch(this IElasticClient client, Id id,
			Func<DeactivateWatchDescriptor, IDeactivateWatchRequest> selector = null
		)
			=> client.Watcher.Deactivate(id, selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static DeactivateWatchResponse DeactivateWatch(this IElasticClient client, IDeactivateWatchRequest request)
			=> client.Watcher.Deactivate(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<DeactivateWatchResponse> DeactivateWatchAsync(this IElasticClient client, Id id,
			Func<DeactivateWatchDescriptor, IDeactivateWatchRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Watcher.DeactivateAsync(id, selector, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<DeactivateWatchResponse> DeactivateWatchAsync(this IElasticClient client, IDeactivateWatchRequest request,
			CancellationToken ct = default
		)
			=> client.Watcher.DeactivateAsync(request, ct);
	}
}
