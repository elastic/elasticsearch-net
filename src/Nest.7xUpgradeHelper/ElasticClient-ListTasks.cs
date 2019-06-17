using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Retrieve information about the tasks currently executing on one or more nodes in the cluster.
		/// </summary>
		/// <param name="selector">A descriptor to further describe the tasks to retrieve information for</param>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static ListTasksResponse ListTasks(this IElasticClient client, Func<ListTasksDescriptor, IListTasksRequest> selector = null)
			=> client.Tasks.List(selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static ListTasksResponse ListTasks(this IElasticClient client, IListTasksRequest request)
			=> client.Tasks.List(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<ListTasksResponse> ListTasksAsync(this IElasticClient client, Func<ListTasksDescriptor, IListTasksRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Tasks.ListAsync(selector, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<ListTasksResponse> ListTasksAsync(this IElasticClient client, IListTasksRequest request, CancellationToken ct = default)
			=> client.Tasks.ListAsync(request, ct);
	}
}
