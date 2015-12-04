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
		/// <para> </para><a href="http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/cluster-health.html">http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/cluster-health.html</a>
		/// </summary>
		/// <param name="selector">An optional descriptor to further describe the cluster health operation</param>
		IClusterHealthResponse ClusterHealth(Func<ClusterHealthDescriptor, IClusterHealthRequest> selector = null);

		/// <inheritdoc/>
		IClusterHealthResponse ClusterHealth(IClusterHealthRequest clusterHealthRequest);

		/// <inheritdoc/>
		Task<IClusterHealthResponse> ClusterHealthAsync(Func<ClusterHealthDescriptor, IClusterHealthRequest> selector = null);

		/// <inheritdoc/>
		Task<IClusterHealthResponse> ClusterHealthAsync(IClusterHealthRequest clusterHealthRequest);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IClusterHealthResponse ClusterHealth(Func<ClusterHealthDescriptor, IClusterHealthRequest> selector = null) =>
			this.ClusterHealth(selector.InvokeOrDefault(new ClusterHealthDescriptor()));

		/// <inheritdoc/>
		public IClusterHealthResponse ClusterHealth(IClusterHealthRequest clusterHealthRequest) => 
			this.Dispatcher.Dispatch<IClusterHealthRequest, ClusterHealthRequestParameters, ClusterHealthResponse>(
				clusterHealthRequest,
				(p, d) => this.LowLevelDispatch.ClusterHealthDispatch<ClusterHealthResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IClusterHealthResponse> ClusterHealthAsync(Func<ClusterHealthDescriptor, IClusterHealthRequest> selector = null) =>
			this.ClusterHealthAsync(selector.InvokeOrDefault(new ClusterHealthDescriptor()));

		/// <inheritdoc/>
		public Task<IClusterHealthResponse> ClusterHealthAsync(IClusterHealthRequest clusterHealthRequest) => 
			this.Dispatcher.DispatchAsync<IClusterHealthRequest, ClusterHealthRequestParameters, ClusterHealthResponse, IClusterHealthResponse>(
				clusterHealthRequest,
				(p, d) => this.LowLevelDispatch.ClusterHealthDispatchAsync<ClusterHealthResponse>(p)
			);
	}
}