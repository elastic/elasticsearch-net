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

		/// <inheritdoc />
		Task<IClusterPendingTasksResponse> ClusterPendingTasksAsync(Func<ClusterPendingTasksDescriptor, IClusterPendingTasksRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc />
		IClusterPendingTasksResponse ClusterPendingTasks(IClusterPendingTasksRequest request);

		/// <inheritdoc />
		Task<IClusterPendingTasksResponse> ClusterPendingTasksAsync(IClusterPendingTasksRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IClusterPendingTasksResponse ClusterPendingTasks(Func<ClusterPendingTasksDescriptor, IClusterPendingTasksRequest> selector = null) =>
			ClusterPendingTasks(selector.InvokeOrDefault(new ClusterPendingTasksDescriptor()));

		/// <inheritdoc />
		public IClusterPendingTasksResponse ClusterPendingTasks(IClusterPendingTasksRequest request) =>
			Dispatcher.Dispatch<IClusterPendingTasksRequest, ClusterPendingTasksRequestParameters, ClusterPendingTasksResponse>(
				request,
				(p, d) => LowLevelDispatch.ClusterPendingTasksDispatch<ClusterPendingTasksResponse>(p)
			);

		/// <inheritdoc />
		public Task<IClusterPendingTasksResponse> ClusterPendingTasksAsync(
			Func<ClusterPendingTasksDescriptor, IClusterPendingTasksRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			ClusterPendingTasksAsync(selector.InvokeOrDefault(new ClusterPendingTasksDescriptor()), cancellationToken);

		/// <inheritdoc />
		public Task<IClusterPendingTasksResponse> ClusterPendingTasksAsync(IClusterPendingTasksRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			Dispatcher
				.DispatchAsync<IClusterPendingTasksRequest, ClusterPendingTasksRequestParameters, ClusterPendingTasksResponse,
					IClusterPendingTasksResponse>(
					request,
					cancellationToken,
					(p, d, c) => LowLevelDispatch.ClusterPendingTasksDispatchAsync<ClusterPendingTasksResponse>(p, c)
				);
	}
}
