using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nest
{
	public partial class ElasticClient
	{
		public IClusterStateResponse ClusterState(Func<ClusterStateDescriptor, ClusterStateDescriptor> clusterStateSelector = null)
		{
			clusterStateSelector = clusterStateSelector ?? (s => s);
			return this.Dispatch<ClusterStateDescriptor, ClusterStateQueryString, ClusterStateResponse>(
				s=> clusterStateSelector(s.FlatSettings()),
				(p, d) => this.RawDispatch.ClusterStateDispatch(p)
			);
		}
		
		public Task<IClusterStateResponse> ClusterStateAsync(Func<ClusterStateDescriptor, ClusterStateDescriptor> clusterStateSelector = null)
		{
			clusterStateSelector = clusterStateSelector ?? (s => s);
			return this
				.DispatchAsync<ClusterStateDescriptor, ClusterStateQueryString, ClusterStateResponse, IClusterStateResponse>(
					s=> clusterStateSelector(s.FlatSettings()),
					(p, d) => this.RawDispatch.ClusterStateDispatchAsync(p)
				);
		}
	}
}
