using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Cluster.PendingTasks(), please update this usage.")]
		public static ClusterPendingTasksResponse ClusterPendingTasks(this IElasticClient client,
			Func<ClusterPendingTasksDescriptor, IClusterPendingTasksRequest> selector = null
		)
			=> client.Cluster.PendingTasks(selector);

		[Obsolete("Moved to client.Cluster.PendingTasksAsync(), please update this usage.")]
		public static Task<ClusterPendingTasksResponse> ClusterPendingTasksAsync(this IElasticClient client,
			Func<ClusterPendingTasksDescriptor, IClusterPendingTasksRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Cluster.PendingTasksAsync(selector, ct);

		[Obsolete("Moved to client.Cluster.PendingTasks(), please update this usage.")]
		public static ClusterPendingTasksResponse ClusterPendingTasks(this IElasticClient client, IClusterPendingTasksRequest request)
			=> client.Cluster.PendingTasks(request);

		[Obsolete("Moved to client.Cluster.PendingTasksAsync(), please update this usage.")]
		public static Task<ClusterPendingTasksResponse> ClusterPendingTasksAsync(this IElasticClient client, IClusterPendingTasksRequest request,
			CancellationToken ct = default
		)
			=> client.Cluster.PendingTasksAsync(request, ct);
	}
}
