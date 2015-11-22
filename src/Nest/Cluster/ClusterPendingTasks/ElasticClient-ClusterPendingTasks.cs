using Elasticsearch.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Returns a list of any cluster-level changes (e.g. create index, update mapping, allocate or fail shard) which have not yet been executed.
		/// </summary>
		IClusterPendingTasksResponse ClusterPendingTasks(Func<ClusterPendingTasksDescriptor, IClusterPendingTasksRequest> pendingTasksSelector = null);

		/// <inheritdoc/>
		Task<IClusterPendingTasksResponse> ClusterPendingTasksAsync(Func<ClusterPendingTasksDescriptor, IClusterPendingTasksRequest> pendingTasksSelector = null);

		/// <inheritdoc/>
		IClusterPendingTasksResponse ClusterPendingTasks(IClusterPendingTasksRequest pendingTasksRequest);

		/// <inheritdoc/>
		Task<IClusterPendingTasksResponse> ClusterPendingTasksAsync(IClusterPendingTasksRequest pendingTasksRequest);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IClusterPendingTasksResponse ClusterPendingTasks(Func<ClusterPendingTasksDescriptor, IClusterPendingTasksRequest> pendingTasksSelector = null) =>
			this.ClusterPendingTasks(pendingTasksSelector.InvokeOrDefault(new ClusterPendingTasksDescriptor()));

		/// <inheritdoc/>
		public Task<IClusterPendingTasksResponse> ClusterPendingTasksAsync(Func<ClusterPendingTasksDescriptor, IClusterPendingTasksRequest> pendingTasksSelector = null) =>
			this.ClusterPendingTasksAsync(pendingTasksSelector.InvokeOrDefault(new ClusterPendingTasksDescriptor()));

		/// <inheritdoc/>
		public IClusterPendingTasksResponse ClusterPendingTasks(IClusterPendingTasksRequest pendingTasksRequest) => 
			this.Dispatcher.Dispatch<IClusterPendingTasksRequest, ClusterPendingTasksRequestParameters, ClusterPendingTasksResponse>(
				pendingTasksRequest,
				(p, d) => this.LowLevelDispatch.ClusterPendingTasksDispatch<ClusterPendingTasksResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IClusterPendingTasksResponse> ClusterPendingTasksAsync(IClusterPendingTasksRequest pendingTasksRequest) => 
			this.Dispatcher.DispatchAsync<IClusterPendingTasksRequest, ClusterPendingTasksRequestParameters, ClusterPendingTasksResponse, IClusterPendingTasksResponse>(
				pendingTasksRequest,
				(p, d) => this.LowLevelDispatch.ClusterPendingTasksDispatchAsync<ClusterPendingTasksResponse>(p)
			);
	}
}
