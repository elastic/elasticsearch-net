using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// The cluster health API allows to get a very simple status on the health of the cluster.
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/cluster-health.html
		/// </summary>
		/// <param name="clusterHealthSelector">An optional descriptor to further describe the cluster health operation</param>
		IClusterHealthResponse ClusterHealth(Func<ClusterHealthDescriptor, IClusterHealthRequest> clusterHealthSelector = null);

		/// <inheritdoc/>
		IClusterHealthResponse ClusterHealth(IClusterHealthRequest clusterHealthRequest);

		/// <inheritdoc/>
		Task<IClusterHealthResponse> ClusterHealthAsync(Func<ClusterHealthDescriptor, IClusterHealthRequest> clusterHealthSelector = null);

		/// <inheritdoc/>
		Task<IClusterHealthResponse> ClusterHealthAsync(IClusterHealthRequest clusterHealthRequest);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IClusterHealthResponse ClusterHealth(Func<ClusterHealthDescriptor, IClusterHealthRequest> clusterHealthSelector = null) =>
			this.ClusterHealth(clusterHealthSelector.InvokeOrDefault(new ClusterHealthDescriptor()));

		/// <inheritdoc/>
		public IClusterHealthResponse ClusterHealth(IClusterHealthRequest clusterHealthRequest) => 
			this.Dispatcher.Dispatch<IClusterHealthRequest, ClusterHealthRequestParameters, ClusterHealthResponse>(
				clusterHealthRequest,
				(p, d) => this.LowLevelDispatch.ClusterHealthDispatch<ClusterHealthResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IClusterHealthResponse> ClusterHealthAsync(Func<ClusterHealthDescriptor, IClusterHealthRequest> clusterHealthSelector = null) =>
			this.ClusterHealthAsync(clusterHealthSelector.InvokeOrDefault(new ClusterHealthDescriptor()));

		/// <inheritdoc/>
		public Task<IClusterHealthResponse> ClusterHealthAsync(IClusterHealthRequest clusterHealthRequest) => 
			this.Dispatcher.DispatchAsync<IClusterHealthRequest, ClusterHealthRequestParameters, ClusterHealthResponse, IClusterHealthResponse>(
				clusterHealthRequest,
				(p, d) => this.LowLevelDispatch.ClusterHealthDispatchAsync<ClusterHealthResponse>(p)
			);
	}
}