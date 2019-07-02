using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Watcher.Execute(), please update this usage.")]
		public static ExecuteWatchResponse ExecuteWatch(this IElasticClient client, Func<ExecuteWatchDescriptor, IExecuteWatchRequest> selector)
			=> client.Watcher.Execute(selector);

		[Obsolete("Moved to client.Watcher.Execute(), please update this usage.")]
		public static ExecuteWatchResponse ExecuteWatch(this IElasticClient client, IExecuteWatchRequest request)
			=> client.Watcher.Execute(request);

		[Obsolete("Moved to client.Watcher.ExecuteAsync(), please update this usage.")]
		public static Task<ExecuteWatchResponse> ExecuteWatchAsync(this IElasticClient client,
			Func<ExecuteWatchDescriptor, IExecuteWatchRequest> selector,
			CancellationToken ct = default
		)
			=> client.Watcher.ExecuteAsync(selector, ct);

		[Obsolete("Moved to client.Watcher.ExecuteAsync(), please update this usage.")]
		public static Task<ExecuteWatchResponse> ExecuteWatchAsync(this IElasticClient client, IExecuteWatchRequest request,
			CancellationToken ct = default
		)
			=> client.Watcher.ExecuteAsync(request, ct);
	}
}
