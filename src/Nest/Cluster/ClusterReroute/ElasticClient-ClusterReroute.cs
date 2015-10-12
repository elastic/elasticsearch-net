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
		/// Allows to explicitly execute a cluster reroute allocation command including specific commands. 
		/// For example, a shard can be moved from one node to another explicitly, an allocation can be canceled, 
		/// or an unassigned shard can be explicitly allocated on a specific node.
		/// </summary>
		IClusterRerouteResponse ClusterReroute(Func<ClusterRerouteDescriptor, IClusterRerouteRequest> clusterRerouteSelector);

		/// <inheritdoc/>
		Task<IClusterRerouteResponse> ClusterRerouteAsync(Func<ClusterRerouteDescriptor, IClusterRerouteRequest> clusterRerouteSelector);

		/// <inheritdoc/>
		IClusterRerouteResponse ClusterReroute(IClusterRerouteRequest clusterRerouteRequest);

		/// <inheritdoc/>
		Task<IClusterRerouteResponse> ClusterRerouteAsync(IClusterRerouteRequest clusterRerouteRequest);

	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IClusterRerouteResponse ClusterReroute(Func<ClusterRerouteDescriptor, IClusterRerouteRequest> clusterRerouteSelector) =>
			this.ClusterReroute(clusterRerouteSelector?.Invoke(new ClusterRerouteDescriptor()));

		/// <inheritdoc/>
		public Task<IClusterRerouteResponse> ClusterRerouteAsync(Func<ClusterRerouteDescriptor, IClusterRerouteRequest> clusterRerouteSelector) =>
			this.ClusterRerouteAsync(clusterRerouteSelector?.Invoke(new ClusterRerouteDescriptor()));

		/// <inheritdoc/>
		public IClusterRerouteResponse ClusterReroute(IClusterRerouteRequest clusterRerouteRequest) => 
			this.Dispatcher.Dispatch<IClusterRerouteRequest, ClusterRerouteRequestParameters, ClusterRerouteResponse>(
				clusterRerouteRequest,
				this.LowLevelDispatch.ClusterRerouteDispatch<ClusterRerouteResponse>
			);

		/// <inheritdoc/>
		public Task<IClusterRerouteResponse> ClusterRerouteAsync(IClusterRerouteRequest clusterRerouteRequest) => 
			this.Dispatcher.DispatchAsync<IClusterRerouteRequest, ClusterRerouteRequestParameters, ClusterRerouteResponse, IClusterRerouteResponse>(
				clusterRerouteRequest,
				this.LowLevelDispatch.ClusterRerouteDispatchAsync<ClusterRerouteResponse>
			);
	}
}
