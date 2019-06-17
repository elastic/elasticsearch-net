using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Activates a currently inactive watch.
		/// </summary>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static ActivateWatchResponse ActivateWatch(this IElasticClient client, Id id,
			Func<ActivateWatchDescriptor, IActivateWatchRequest> selector = null
		)
			=> client.Watcher.Activate(id, selector);

		////<inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static ActivateWatchResponse ActivateWatch(this IElasticClient client, IActivateWatchRequest request)
			=> client.Watcher.Activate(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<ActivateWatchResponse> ActivateWatchAsync(this IElasticClient client, Id id,
			Func<ActivateWatchDescriptor, IActivateWatchRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Watcher.ActivateAsync(id, selector, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<ActivateWatchResponse> ActivateWatchAsync(this IElasticClient client, IActivateWatchRequest request,
			CancellationToken ct = default
		)
			=> client.Watcher.ActivateAsync(request, ct);
	}
}
