using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// The cluster state API allows to get a comprehensive state information of the whole cluster.
		/// <para> </para><a href="http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/cluster-state.html">http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/cluster-state.html</a>
		/// </summary>
		/// <param name="selector">A descriptor that describes the parameters for the cluster state operation</param>
		IClusterStateResponse ClusterState(Func<ClusterStateDescriptor, IClusterStateRequest> selector = null);

		/// <inheritdoc/>
		IClusterStateResponse ClusterState(IClusterStateRequest clusterStateRequest);

		/// <inheritdoc/>
		Task<IClusterStateResponse> ClusterStateAsync(Func<ClusterStateDescriptor, IClusterStateRequest> selector = null);

		/// <inheritdoc/>
		Task<IClusterStateResponse> ClusterStateAsync(IClusterStateRequest clusterStateRequest);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IClusterStateResponse ClusterState(Func<ClusterStateDescriptor, IClusterStateRequest> selector = null) =>
			this.ClusterState(selector.InvokeOrDefault(new ClusterStateDescriptor()));

		/// <inheritdoc/>
		public IClusterStateResponse ClusterState(IClusterStateRequest clusterStateRequest) => 
			this.Dispatcher.Dispatch<IClusterStateRequest, ClusterStateRequestParameters, ClusterStateResponse>(
				clusterStateRequest,
				(p, d) => this.LowLevelDispatch.ClusterStateDispatch<ClusterStateResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IClusterStateResponse> ClusterStateAsync(Func<ClusterStateDescriptor, IClusterStateRequest> selector = null) =>
			this.ClusterStateAsync(selector.InvokeOrDefault(new ClusterStateDescriptor()));

		/// <inheritdoc/>
		public Task<IClusterStateResponse> ClusterStateAsync(IClusterStateRequest clusterStateRequest) => 
			this.Dispatcher.DispatchAsync<IClusterStateRequest, ClusterStateRequestParameters, ClusterStateResponse, IClusterStateResponse>(
				clusterStateRequest,
				(p, d) => this.LowLevelDispatch.ClusterStateDispatchAsync<ClusterStateResponse>(p)
			);
	}
}