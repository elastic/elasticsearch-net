using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static CatResponse<CatPendingTasksRecord> CatPendingTasks(this IElasticClient client,
			Func<CatPendingTasksDescriptor, ICatPendingTasksRequest> selector = null
		)
			=> client.Cat.PendingTasks(selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static CatResponse<CatPendingTasksRecord> CatPendingTasks(this IElasticClient client, ICatPendingTasksRequest request)
			=> client.Cat.PendingTasks(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<CatResponse<CatPendingTasksRecord>> CatPendingTasksAsync(this IElasticClient client,
			Func<CatPendingTasksDescriptor, ICatPendingTasksRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Cat.PendingTasksAsync(selector, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<CatResponse<CatPendingTasksRecord>> CatPendingTasksAsync(this IElasticClient client, ICatPendingTasksRequest request,
			CancellationToken ct = default
		)
			=> client.Cat.PendingTasksAsync(request);
	}
}
