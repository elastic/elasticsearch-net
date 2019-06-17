using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Cat.PendingTasks(), please update this usage.")]
		public static CatResponse<CatPendingTasksRecord> CatPendingTasks(this IElasticClient client,
			Func<CatPendingTasksDescriptor, ICatPendingTasksRequest> selector = null
		)
			=> client.Cat.PendingTasks(selector);

		[Obsolete("Moved to client.Cat.PendingTasks(), please update this usage.")]
		public static CatResponse<CatPendingTasksRecord> CatPendingTasks(this IElasticClient client, ICatPendingTasksRequest request)
			=> client.Cat.PendingTasks(request);

		[Obsolete("Moved to client.Cat.PendingTasksAsync(), please update this usage.")]
		public static Task<CatResponse<CatPendingTasksRecord>> CatPendingTasksAsync(this IElasticClient client,
			Func<CatPendingTasksDescriptor, ICatPendingTasksRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Cat.PendingTasksAsync(selector, ct);

		[Obsolete("Moved to client.Cat.PendingTasksAsync(), please update this usage.")]
		public static Task<CatResponse<CatPendingTasksRecord>> CatPendingTasksAsync(this IElasticClient client, ICatPendingTasksRequest request,
			CancellationToken ct = default
		)
			=> client.Cat.PendingTasksAsync(request, ct);
	}
}
