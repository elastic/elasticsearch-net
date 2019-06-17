using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Forces the execution of a stored watch. It can be used to force execution of the watch outside of its triggering logic,
		/// or to simulate the watch execution for debugging purposes.
		/// </summary>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static ExecuteWatchResponse ExecuteWatch(this IElasticClient client, Func<ExecuteWatchDescriptor, IExecuteWatchRequest> selector)
			=> client.Watcher.Execute(selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static ExecuteWatchResponse ExecuteWatch(this IElasticClient client, IExecuteWatchRequest request)
			=> client.Watcher.Execute(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<ExecuteWatchResponse> ExecuteWatchAsync(this IElasticClient client,
			Func<ExecuteWatchDescriptor, IExecuteWatchRequest> selector,
			CancellationToken ct = default
		)
			=> client.Watcher.ExecuteAsync(selector, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<ExecuteWatchResponse> ExecuteWatchAsync(this IElasticClient client, IExecuteWatchRequest request,
			CancellationToken ct = default
		)
			=> client.Watcher.ExecuteAsync(request, ct);
	}
}
