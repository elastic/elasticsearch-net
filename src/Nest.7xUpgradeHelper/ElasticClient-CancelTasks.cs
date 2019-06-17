using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Does a request to the root of an elasticsearch node
		/// </summary>
		/// <param name="selector">A descriptor to further describe the root operation</param>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static CancelTasksResponse CancelTasks(this IElasticClient client, Func<CancelTasksDescriptor, ICancelTasksRequest> selector = null)
			=> client.Tasks.Cancel(selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static CancelTasksResponse CancelTasks(this IElasticClient client, ICancelTasksRequest request)
			=> client.Tasks.Cancel(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<CancelTasksResponse> CancelTasksAsync(this IElasticClient client,
			Func<CancelTasksDescriptor, ICancelTasksRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Tasks.CancelAsync(selector, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<CancelTasksResponse> CancelTasksAsync(this IElasticClient client, ICancelTasksRequest request,
			CancellationToken ct = default
		)
			=> client.Tasks.CancelAsync(request, ct);
	}
}
