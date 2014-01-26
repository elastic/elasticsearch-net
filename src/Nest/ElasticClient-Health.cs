using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Nest
{
	public partial class ElasticClient
	{
		public IHealthResponse Health(Func<ClusterHealthDescriptor, ClusterHealthDescriptor> clusterHealthSelector)
		{
			return this.Dispatch<ClusterHealthDescriptor, ClusterHealthQueryString, HealthResponse>(
				clusterHealthSelector,
				(p, d) => this.RawDispatch.ClusterHealthDispatch(p)
			);
		}
		
		public Task<IHealthResponse> HealthAsync(Func<ClusterHealthDescriptor, ClusterHealthDescriptor> clusterHealthSelector)
		{
			return this.DispatchAsync<ClusterHealthDescriptor, ClusterHealthQueryString, HealthResponse, IHealthResponse>(
				clusterHealthSelector,
				(p, d) => this.RawDispatch.ClusterHealthDispatchAsync(p)
			);
		}
	}
}
