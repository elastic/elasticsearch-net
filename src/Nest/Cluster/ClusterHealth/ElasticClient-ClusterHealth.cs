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
		IHealthResponse ClusterHealth(Func<ClusterHealthDescriptor, IClusterHealthRequest> clusterHealthSelector = null);

		/// <inheritdoc/>
		IHealthResponse ClusterHealth(IClusterHealthRequest clusterHealthRequest);

		/// <inheritdoc/>
		Task<IHealthResponse> ClusterHealthAsync(Func<ClusterHealthDescriptor, IClusterHealthRequest> clusterHealthSelector = null);

		/// <inheritdoc/>
		Task<IHealthResponse> ClusterHealthAsync(IClusterHealthRequest clusterHealthRequest);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IHealthResponse ClusterHealth(Func<ClusterHealthDescriptor, IClusterHealthRequest> clusterHealthSelector = null) =>
			this.ClusterHealth(clusterHealthSelector.InvokeOrDefault(new ClusterHealthDescriptor()));

		/// <inheritdoc/>
		public IHealthResponse ClusterHealth(IClusterHealthRequest clusterHealthRequest) => 
			this.Dispatcher.Dispatch<IClusterHealthRequest, ClusterHealthRequestParameters, HealthResponse>(
				clusterHealthRequest,
				(p, d) => this.LowLevelDispatch.ClusterHealthDispatch<HealthResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IHealthResponse> ClusterHealthAsync(Func<ClusterHealthDescriptor, IClusterHealthRequest> clusterHealthSelector = null) =>
			this.ClusterHealthAsync(clusterHealthSelector.InvokeOrDefault(new ClusterHealthDescriptor()));

		/// <inheritdoc/>
		public Task<IHealthResponse> ClusterHealthAsync(IClusterHealthRequest clusterHealthRequest) => 
			this.Dispatcher.DispatchAsync<IClusterHealthRequest, ClusterHealthRequestParameters, HealthResponse, IHealthResponse>(
				clusterHealthRequest,
				(p, d) => this.LowLevelDispatch.ClusterHealthDispatchAsync<HealthResponse>(p)
			);
	}
}