using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Watcher.Delete(), please update this usage.")]
		public static DeleteWatchResponse DeleteWatch(this IElasticClient client, Id watchId,
			Func<DeleteWatchDescriptor, IDeleteWatchRequest> selector = null
		)
			=> client.Watcher.Delete(watchId, selector);

		[Obsolete("Moved to client.Watcher.Delete(), please update this usage.")]
		public static DeleteWatchResponse DeleteWatch(this IElasticClient client, IDeleteWatchRequest request)
			=> client.Watcher.Delete(request);

		[Obsolete("Moved to client.Watcher.DeleteAsync(), please update this usage.")]
		public static Task<DeleteWatchResponse> DeleteWatchAsync(this IElasticClient client, Id watchId,
			Func<DeleteWatchDescriptor, IDeleteWatchRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Watcher.DeleteAsync(watchId, selector, ct);

		[Obsolete("Moved to client.Watcher.DeleteAsync(), please update this usage.")]
		public static Task<DeleteWatchResponse> DeleteWatchAsync(this IElasticClient client, IDeleteWatchRequest request,
			CancellationToken ct = default
		)
			=> client.Watcher.DeleteAsync(request, ct);
	}
}
