using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	internal static class InvokeExtensions
	{
		internal static TReturn InvokeOrDefault<T, TReturn>(this Func<T, TReturn> func, T @default)
			where T : class, TReturn where TReturn : class =>
			func?.Invoke(@default) ?? @default;
	}
	
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static AcknowledgeWatchResponse AcknowledgeWatch(this IElasticClient client, Id id,
			Func<AcknowledgeWatchDescriptor, IAcknowledgeWatchRequest> selector = null
		)
			=> client.Watcher.Acknowledge(id, selector);

		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static AcknowledgeWatchResponse AcknowledgeWatch(this IElasticClient client, IAcknowledgeWatchRequest request)
			=> client.Watcher.Acknowledge(request);

		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<AcknowledgeWatchResponse> AcknowledgeWatchAsync(this IElasticClient client, Id id,
			Func<AcknowledgeWatchDescriptor, IAcknowledgeWatchRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Watcher.AcknowledgeAsync(id, selector, ct);

		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<AcknowledgeWatchResponse> AcknowledgeWatchAsync(this IElasticClient client, IAcknowledgeWatchRequest request,
			CancellationToken ct = default
		)
			=> client.Watcher.AcknowledgeAsync(request, ct);
	}
}
