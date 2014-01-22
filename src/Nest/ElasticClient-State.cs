using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nest
{
	public partial class ElasticClient
	{
		public IClusterStateResponse ClusterState(Func<ClusterStateDescriptor, ClusterStateDescriptor> clusterStateSelector)
		{	
			return this.Dispatch<ClusterStateDescriptor, ClusterStateQueryString, ClusterStateResponse>(
				clusterStateSelector,
				(p, d) => this.RawDispatch.ClusterStateDispatch(p)
			);
		}
		
		public Task<IClusterStateResponse> ClusterStateAsync(Func<ClusterStateDescriptor, ClusterStateDescriptor> clusterStateSelector)
		{
			return this
				.DispatchAsync<ClusterStateDescriptor, ClusterStateQueryString, ClusterStateResponse, IClusterStateResponse>(
					clusterStateSelector,
					(p, d) => this.RawDispatch.ClusterStateDispatchAsync(p)
				);
		}
	}
}
