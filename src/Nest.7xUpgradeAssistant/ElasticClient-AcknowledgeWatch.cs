using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Watcher.Acknowledge(), please update this usage.")]
		public static AcknowledgeWatchResponse AcknowledgeWatch(this IElasticClient client, Id id,
			Func<AcknowledgeWatchDescriptor, IAcknowledgeWatchRequest> selector = null
		)
			=> client.Watcher.Acknowledge(id, selector);

		[Obsolete("Moved to client.Watcher.Acknowledge(), please update this usage.")]
		public static AcknowledgeWatchResponse AcknowledgeWatch(this IElasticClient client, IAcknowledgeWatchRequest request)
			=> client.Watcher.Acknowledge(request);

		[Obsolete("Moved to client.Watcher.AcknowledgeAsync(), please update this usage.")]
		public static Task<AcknowledgeWatchResponse> AcknowledgeWatchAsync(this IElasticClient client, Id id,
			Func<AcknowledgeWatchDescriptor, IAcknowledgeWatchRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Watcher.AcknowledgeAsync(id, selector, ct);

		[Obsolete("Moved to client.Watcher.AcknowledgeAsync(), please update this usage.")]
		public static Task<AcknowledgeWatchResponse> AcknowledgeWatchAsync(this IElasticClient client, IAcknowledgeWatchRequest request,
			CancellationToken ct = default
		)
			=> client.Watcher.AcknowledgeAsync(request, ct);
	}
}
