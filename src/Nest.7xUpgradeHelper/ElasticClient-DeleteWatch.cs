using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Removes a watch identified by its id from Watcher. Once removed, the document representing the watch in the .watches
		/// index is gone
		/// and it will never be executed again.
		/// </summary>
		/// <remarks>
		/// Deleting a watch does not delete any watch execution records related to this watch from the watch history.
		/// </remarks>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static DeleteWatchResponse DeleteWatch(this IElasticClient client, Id watchId,
			Func<DeleteWatchDescriptor, IDeleteWatchRequest> selector = null
		)
			=> client.Watcher.Delete(watchId, selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static DeleteWatchResponse DeleteWatch(this IElasticClient client, IDeleteWatchRequest request)
			=> client.Watcher.Delete(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<DeleteWatchResponse> DeleteWatchAsync(this IElasticClient client, Id watchId,
			Func<DeleteWatchDescriptor, IDeleteWatchRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Watcher.DeleteAsync(watchId, selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<DeleteWatchResponse> DeleteWatchAsync(this IElasticClient client, IDeleteWatchRequest request,
			CancellationToken ct = default
		)
			=> client.Watcher.DeleteAsync(request, ct);
	}
}
