using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Tasks.List(), please update this usage.")]
		public static ListTasksResponse ListTasks(this IElasticClient client, Func<ListTasksDescriptor, IListTasksRequest> selector = null)
			=> client.Tasks.List(selector);

		[Obsolete("Moved to client.Tasks.List(), please update this usage.")]
		public static ListTasksResponse ListTasks(this IElasticClient client, IListTasksRequest request)
			=> client.Tasks.List(request);

		[Obsolete("Moved to client.Tasks.ListAsync(), please update this usage.")]
		public static Task<ListTasksResponse> ListTasksAsync(this IElasticClient client, Func<ListTasksDescriptor, IListTasksRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Tasks.ListAsync(selector, ct);

		[Obsolete("Moved to client.Tasks.ListAsync(), please update this usage.")]
		public static Task<ListTasksResponse> ListTasksAsync(this IElasticClient client, IListTasksRequest request, CancellationToken ct = default)
			=> client.Tasks.ListAsync(request, ct);
	}
}
