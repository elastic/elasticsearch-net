using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Cat.Tasks(), please update this usage.")]
		public static CatResponse<CatTasksRecord> CatTasks(this IElasticClient client, Func<CatTasksDescriptor, ICatTasksRequest> selector = null)
			=> client.Cat.Tasks(selector);

		[Obsolete("Moved to client.Cat.Tasks(), please update this usage.")]
		public static CatResponse<CatTasksRecord> CatTasks(this IElasticClient client, ICatTasksRequest request)
			=> client.Cat.Tasks(request);

		[Obsolete("Moved to client.Cat.TasksAsync(), please update this usage.")]
		public static Task<CatResponse<CatTasksRecord>> CatTasksAsync(this IElasticClient client,
			Func<CatTasksDescriptor, ICatTasksRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Cat.TasksAsync(selector, ct);

		[Obsolete("Moved to client.Cat.TasksAsync(), please update this usage.")]
		public static Task<CatResponse<CatTasksRecord>> CatTasksAsync(this IElasticClient client, ICatTasksRequest request,
			CancellationToken ct = default
		)
			=> client.Cat.TasksAsync(request, ct);
	}
}
