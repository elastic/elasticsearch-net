using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Tasks.Cancel(), please update this usage.")]
		public static CancelTasksResponse CancelTasks(this IElasticClient client, Func<CancelTasksDescriptor, ICancelTasksRequest> selector = null)
			=> client.Tasks.Cancel(selector);

		[Obsolete("Moved to client.Tasks.Cancel(), please update this usage.")]
		public static CancelTasksResponse CancelTasks(this IElasticClient client, ICancelTasksRequest request)
			=> client.Tasks.Cancel(request);

		[Obsolete("Moved to client.Tasks.CancelAsync(), please update this usage.")]
		public static Task<CancelTasksResponse> CancelTasksAsync(this IElasticClient client,
			Func<CancelTasksDescriptor, ICancelTasksRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Tasks.CancelAsync(selector, ct);

		[Obsolete("Moved to client.Tasks.CancelAsync(), please update this usage.")]
		public static Task<CancelTasksResponse> CancelTasksAsync(this IElasticClient client, ICancelTasksRequest request,
			CancellationToken ct = default
		)
			=> client.Tasks.CancelAsync(request, ct);
	}
}
