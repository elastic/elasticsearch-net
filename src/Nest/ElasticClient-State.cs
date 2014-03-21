using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IClusterStateResponse ClusterState(
			Func<ClusterStateDescriptor, ClusterStateDescriptor> clusterStateSelector = null)
		{
			clusterStateSelector = clusterStateSelector ?? (s => s);
			return this.Dispatch<ClusterStateDescriptor, ClusterStateQueryString, ClusterStateResponse>(
				clusterStateSelector,
				(p, d) => this.RawDispatch.ClusterStateDispatch<ClusterStateResponse>(p)
			);
		}

		/// <inheritdoc />
		public Task<IClusterStateResponse> ClusterStateAsync(
			Func<ClusterStateDescriptor, ClusterStateDescriptor> clusterStateSelector = null)
		{
			clusterStateSelector = clusterStateSelector ?? (s => s);
			return this.DispatchAsync<ClusterStateDescriptor, ClusterStateQueryString, ClusterStateResponse, IClusterStateResponse>(
				clusterStateSelector,
				(p, d) => this.RawDispatch.ClusterStateDispatchAsync<ClusterStateResponse>(p)
			);
		}
	}
}