using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IHealthResponse ClusterHealth(Func<ClusterHealthDescriptor, ClusterHealthDescriptor> clusterHealthSelector = null)
		{
			clusterHealthSelector = clusterHealthSelector ?? (s => s);
			return this.Dispatch<ClusterHealthDescriptor, ClusterHealthRequestParameters, HealthResponse>(
				clusterHealthSelector,
				(p, d) => this.RawDispatch.ClusterHealthDispatch<HealthResponse>(p)
			);
		}

		/// <inheritdoc />
		public IHealthResponse ClusterHealth(IClusterHealthRequest clusterHealthRequest)
		{
			return this.Dispatch<IClusterHealthRequest, ClusterHealthRequestParameters, HealthResponse>(
				clusterHealthRequest,
				(p, d) => this.RawDispatch.ClusterHealthDispatch<HealthResponse>(p)
			);
		}

		/// <inheritdoc />
		public Task<IHealthResponse> ClusterHealthAsync(Func<ClusterHealthDescriptor, ClusterHealthDescriptor> clusterHealthSelector = null)
		{
			clusterHealthSelector = clusterHealthSelector ?? (s => s);
			return this.DispatchAsync<ClusterHealthDescriptor, ClusterHealthRequestParameters, HealthResponse, IHealthResponse>(
				clusterHealthSelector,
				(p, d) => this.RawDispatch.ClusterHealthDispatchAsync<HealthResponse>(p)
			);
		}

		/// <inheritdoc />
		public Task<IHealthResponse> ClusterHealthAsync(IClusterHealthRequest clusterHealthRequest)
		{
			return this.DispatchAsync<IClusterHealthRequest, ClusterHealthRequestParameters, HealthResponse, IHealthResponse>(
				clusterHealthRequest,
				(p, d) => this.RawDispatch.ClusterHealthDispatchAsync<HealthResponse>(p)
			);
		}

	}
}