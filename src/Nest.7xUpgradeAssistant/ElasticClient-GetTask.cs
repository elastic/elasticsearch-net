using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Tasks.GetTask(), please update this usage.")]
		public static GetTaskResponse GetTask(this IElasticClient client, TaskId id, Func<GetTaskDescriptor, IGetTaskRequest> selector = null)
			=> client.Tasks.GetTask(id, selector);

		[Obsolete("Moved to client.Tasks.GetTask(), please update this usage.")]
		public static GetTaskResponse GetTask(this IElasticClient client, IGetTaskRequest request)
			=> client.Tasks.GetTask(request);

		[Obsolete("Moved to client.Tasks.GetTaskAsync(), please update this usage.")]
		public static Task<GetTaskResponse> GetTaskAsync(this IElasticClient client,
			TaskId id,
			Func<GetTaskDescriptor, IGetTaskRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Tasks.GetTaskAsync(id, selector, ct);

		[Obsolete("Moved to client.Tasks.GetTaskAsync(), please update this usage.")]
		public static Task<GetTaskResponse> GetTaskAsync(this IElasticClient client, IGetTaskRequest request, CancellationToken ct = default)
			=> client.Tasks.GetTaskAsync(request, ct);
	}
}
