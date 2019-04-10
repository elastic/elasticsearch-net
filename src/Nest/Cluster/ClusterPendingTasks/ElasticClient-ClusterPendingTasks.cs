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
		IClusterPendingTasksResponse ClusterPendingTasks(Func<ClusterPendingTasksDescriptor, IClusterPendingTasksRequest> selector = null);

		/// <inheritdoc cref="ClusterPendingTasks(System.Func{Nest.ClusterPendingTasksDescriptor,Nest.IClusterPendingTasksRequest})" />
		Task<IClusterPendingTasksResponse> ClusterPendingTasksAsync(
			Func<ClusterPendingTasksDescriptor, IClusterPendingTasksRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc cref="ClusterPendingTasks(System.Func{Nest.ClusterPendingTasksDescriptor,Nest.IClusterPendingTasksRequest})" />
		IClusterPendingTasksResponse ClusterPendingTasks(IClusterPendingTasksRequest request);

		/// <inheritdoc cref="ClusterPendingTasks(System.Func{Nest.ClusterPendingTasksDescriptor,Nest.IClusterPendingTasksRequest})" />
		Task<IClusterPendingTasksResponse> ClusterPendingTasksAsync(IClusterPendingTasksRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc cref="ClusterPendingTasks(System.Func{Nest.ClusterPendingTasksDescriptor,Nest.IClusterPendingTasksRequest})" />
		public IClusterPendingTasksResponse ClusterPendingTasks(Func<ClusterPendingTasksDescriptor, IClusterPendingTasksRequest> selector = null) =>
			ClusterPendingTasks(selector.InvokeOrDefault(new ClusterPendingTasksDescriptor()));

		/// <inheritdoc cref="ClusterPendingTasks(System.Func{Nest.ClusterPendingTasksDescriptor,Nest.IClusterPendingTasksRequest})" />
		public IClusterPendingTasksResponse ClusterPendingTasks(IClusterPendingTasksRequest request) =>
			DoRequest<IClusterPendingTasksRequest, ClusterPendingTasksResponse>(request, request.RequestParameters);

		/// <inheritdoc cref="ClusterPendingTasks(System.Func{Nest.ClusterPendingTasksDescriptor,Nest.IClusterPendingTasksRequest})" />
		public Task<IClusterPendingTasksResponse> ClusterPendingTasksAsync(
			Func<ClusterPendingTasksDescriptor, IClusterPendingTasksRequest> selector = null,
			CancellationToken ct = default
		) =>
			ClusterPendingTasksAsync(selector.InvokeOrDefault(new ClusterPendingTasksDescriptor()), ct);

		/// <inheritdoc cref="ClusterPendingTasks(System.Func{Nest.ClusterPendingTasksDescriptor,Nest.IClusterPendingTasksRequest})" />
		public Task<IClusterPendingTasksResponse> ClusterPendingTasksAsync(IClusterPendingTasksRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IClusterPendingTasksRequest, IClusterPendingTasksResponse, ClusterPendingTasksResponse>(request, request.RequestParameters, ct);
	}
}
