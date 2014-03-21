using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IHealthResponse ClusterHealth(
			Func<ClusterHealthDescriptor, ClusterHealthDescriptor> clusterHealthSelector = null)
		{
			clusterHealthSelector = clusterHealthSelector ?? (s => s);
			return this.Dispatch<ClusterHealthDescriptor, ClusterHealthQueryString, HealthResponse>(
				clusterHealthSelector,
				(p, d) => this.RawDispatch.ClusterHealthDispatch<HealthResponse>(p)
			);
		}

		/// <inheritdoc />
		public Task<IHealthResponse> ClusterHealthAsync(
			Func<ClusterHealthDescriptor, ClusterHealthDescriptor> clusterHealthSelector = null)
		{
			clusterHealthSelector = clusterHealthSelector ?? (s => s);
			return this.DispatchAsync<ClusterHealthDescriptor, ClusterHealthQueryString, HealthResponse, IHealthResponse>(
				clusterHealthSelector,
				(p, d) => this.RawDispatch.ClusterHealthDispatchAsync<HealthResponse>(p)
			);
		}
	}
}