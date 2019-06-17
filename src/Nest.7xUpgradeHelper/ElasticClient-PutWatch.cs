using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Registers a new watch in Watcher or updates an existing one.
		/// Once registered, a new document will be added to the .watches index, representing the watch,
		/// and its trigger will immediately be registered with the relevant trigger engine.
		/// </summary>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static PutWatchResponse PutWatch(this IElasticClient client, Id watchId, Func<PutWatchDescriptor, IPutWatchRequest> selector = null)
			=> client.Watcher.Put(watchId, selector);

		/// <inheritdoc cref="PutWatch(Nest.Id,System.Func{Nest.PutWatchDescriptor,Nest.IPutWatchRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static PutWatchResponse PutWatch(this IElasticClient client, IPutWatchRequest request)
			=> client.Watcher.Put(request);

		/// <inheritdoc cref="PutWatch(Nest.Id,System.Func{Nest.PutWatchDescriptor,Nest.IPutWatchRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<PutWatchResponse> PutWatchAsync(this IElasticClient client, Id watchId,
			Func<PutWatchDescriptor, IPutWatchRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Watcher.PutAsync(watchId, selector, ct);

		/// <inheritdoc cref="PutWatch(Nest.Id,System.Func{Nest.PutWatchDescriptor,Nest.IPutWatchRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<PutWatchResponse> PutWatchAsync(this IElasticClient client, IPutWatchRequest request, CancellationToken ct = default)
			=> client.Watcher.PutAsync(request, ct);
	}
}
