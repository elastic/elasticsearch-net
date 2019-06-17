using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Returns a list of any cluster-level changes (e.g. create index, update mapping, allocate or fail shard) which have not
		/// yet been executed.
		/// <para> </para>
		/// <a href="https://www.elastic.co/guide/en/elasticsearch/reference/current/cluster-pending.html">https://www.elastic.co/guide/en/elasticsearch/reference/current/cluster-pending.html</a>
		/// </summary>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static ClusterPendingTasksResponse ClusterPendingTasks(this IElasticClient client,
			Func<ClusterPendingTasksDescriptor, IClusterPendingTasksRequest> selector = null
		)
			=> client.Cluster.PendingTasks(selector);

		/// <inheritdoc
		///     cref="ClusterPendingTasks(System.Func{Nest.ClusterPendingTasksDescriptor,Nest.IClusterPendingTasksRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<ClusterPendingTasksResponse> ClusterPendingTasksAsync(this IElasticClient client,
			Func<ClusterPendingTasksDescriptor, IClusterPendingTasksRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Cluster.PendingTasksAsync(selector, ct);

		/// <inheritdoc
		///     cref="ClusterPendingTasks(System.Func{Nest.ClusterPendingTasksDescriptor,Nest.IClusterPendingTasksRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static ClusterPendingTasksResponse ClusterPendingTasks(this IElasticClient client, IClusterPendingTasksRequest request)
			=> client.Cluster.PendingTasks(request);

		/// <inheritdoc
		///     cref="ClusterPendingTasks(System.Func{Nest.ClusterPendingTasksDescriptor,Nest.IClusterPendingTasksRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<ClusterPendingTasksResponse> ClusterPendingTasksAsync(this IElasticClient client, IClusterPendingTasksRequest request,
			CancellationToken ct = default
		)
			=> client.Cluster.PendingTasksAsync(request, ct);
	}
}
