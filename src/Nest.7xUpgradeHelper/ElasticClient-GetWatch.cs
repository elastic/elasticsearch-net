using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Retrieves a watch by its id
		/// </summary>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static GetWatchResponse GetWatch(this IElasticClient client, Id watchId, Func<GetWatchDescriptor, IGetWatchRequest> selector = null)
			=> client.Watcher.Get(watchId, selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static GetWatchResponse GetWatch(this IElasticClient client, IGetWatchRequest request)
			=> client.Watcher.Get(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<GetWatchResponse> GetWatchAsync(this IElasticClient client, Id watchId,
			Func<GetWatchDescriptor, IGetWatchRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Watcher.GetAsync(watchId, selector, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<GetWatchResponse> GetWatchAsync(this IElasticClient client, IGetWatchRequest request, CancellationToken ct = default)
			=> client.Watcher.GetAsync(request);
	}
}
