using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Returns a list of any cluster-level changes (e.g. create index, update mapping, allocate or fail shard) which have not yet been executed.
		/// <para> </para>
		/// <a href="https://www.elastic.co/guide/en/elasticsearch/reference/current/cluster-pending.html">https://www.elastic.co/guide/en/elasticsearch/reference/current/cluster-pending.html</a>
		/// </summary>
		public static ClusterPendingTasksResponse ClusterPendingTasks(this IElasticClient client,Func<ClusterPendingTasksDescriptor, IClusterPendingTasksRequest> selector = null);

		/// <inheritdoc cref="ClusterPendingTasks(System.Func{Nest.ClusterPendingTasksDescriptor,Nest.IClusterPendingTasksRequest})" />
		public static Task<ClusterPendingTasksResponse> ClusterPendingTasksAsync(this IElasticClient client,
			Func<ClusterPendingTasksDescriptor, IClusterPendingTasksRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc cref="ClusterPendingTasks(System.Func{Nest.ClusterPendingTasksDescriptor,Nest.IClusterPendingTasksRequest})" />
		public static ClusterPendingTasksResponse ClusterPendingTasks(this IElasticClient client,IClusterPendingTasksRequest request);

		/// <inheritdoc cref="ClusterPendingTasks(System.Func{Nest.ClusterPendingTasksDescriptor,Nest.IClusterPendingTasksRequest})" />
		public static Task<ClusterPendingTasksResponse> ClusterPendingTasksAsync(this IElasticClient client,IClusterPendingTasksRequest request,
			CancellationToken ct = default
		);
	}

}
