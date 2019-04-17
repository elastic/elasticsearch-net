using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Returns a list of any cluster-level changes (e.g. create index, update mapping, allocate or fail shard) which have not yet been executed.
		/// <para> </para>
		/// <a href="https://www.elastic.co/guide/en/elasticsearch/reference/current/cluster-pending.html">https://www.elastic.co/guide/en/elasticsearch/reference/current/cluster-pending.html</a>
		/// </summary>
		ClusterPendingTasksResponse ClusterPendingTasks(Func<ClusterPendingTasksDescriptor, IClusterPendingTasksRequest> selector = null);

		/// <inheritdoc cref="ClusterPendingTasks(System.Func{Nest.ClusterPendingTasksDescriptor,Nest.IClusterPendingTasksRequest})" />
		Task<ClusterPendingTasksResponse> ClusterPendingTasksAsync(
			Func<ClusterPendingTasksDescriptor, IClusterPendingTasksRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc cref="ClusterPendingTasks(System.Func{Nest.ClusterPendingTasksDescriptor,Nest.IClusterPendingTasksRequest})" />
		ClusterPendingTasksResponse ClusterPendingTasks(IClusterPendingTasksRequest request);

		/// <inheritdoc cref="ClusterPendingTasks(System.Func{Nest.ClusterPendingTasksDescriptor,Nest.IClusterPendingTasksRequest})" />
		Task<ClusterPendingTasksResponse> ClusterPendingTasksAsync(IClusterPendingTasksRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc cref="ClusterPendingTasks(System.Func{Nest.ClusterPendingTasksDescriptor,Nest.IClusterPendingTasksRequest})" />
		public ClusterPendingTasksResponse ClusterPendingTasks(Func<ClusterPendingTasksDescriptor, IClusterPendingTasksRequest> selector = null) =>
			ClusterPendingTasks(selector.InvokeOrDefault(new ClusterPendingTasksDescriptor()));

		/// <inheritdoc cref="ClusterPendingTasks(System.Func{Nest.ClusterPendingTasksDescriptor,Nest.IClusterPendingTasksRequest})" />
		public ClusterPendingTasksResponse ClusterPendingTasks(IClusterPendingTasksRequest request) =>
			DoRequest<IClusterPendingTasksRequest, ClusterPendingTasksResponse>(request, request.RequestParameters);

		/// <inheritdoc cref="ClusterPendingTasks(System.Func{Nest.ClusterPendingTasksDescriptor,Nest.IClusterPendingTasksRequest})" />
		public Task<ClusterPendingTasksResponse> ClusterPendingTasksAsync(
			Func<ClusterPendingTasksDescriptor, IClusterPendingTasksRequest> selector = null,
			CancellationToken ct = default
		) =>
			ClusterPendingTasksAsync(selector.InvokeOrDefault(new ClusterPendingTasksDescriptor()), ct);

		/// <inheritdoc cref="ClusterPendingTasks(System.Func{Nest.ClusterPendingTasksDescriptor,Nest.IClusterPendingTasksRequest})" />
		public Task<ClusterPendingTasksResponse> ClusterPendingTasksAsync(IClusterPendingTasksRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IClusterPendingTasksRequest, ClusterPendingTasksResponse>(request, request.RequestParameters, ct);
	}
}
