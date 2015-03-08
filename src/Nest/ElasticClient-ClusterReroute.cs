using Elasticsearch.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest
{
	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IClusterRerouteResponse ClusterReroute(Func<ClusterRerouteDescriptor, ClusterRerouteDescriptor> clusterRerouteSelector)
		{
			clusterRerouteSelector = clusterRerouteSelector ?? (s => s);
			return this.Dispatch<ClusterRerouteDescriptor, ClusterRerouteRequestParameters, ClusterRerouteResponse>(
				clusterRerouteSelector,
				(p, d) => this.RawDispatch.ClusterRerouteDispatch<ClusterRerouteResponse>(p, d)
			);
		}

		/// <inheritdoc />
		public Task<IClusterRerouteResponse> ClusterRerouteAsync(Func<ClusterRerouteDescriptor, ClusterRerouteDescriptor> clusterRerouteSelector)
		{
			clusterRerouteSelector = clusterRerouteSelector ?? (s => s);
			return this.DispatchAsync<ClusterRerouteDescriptor, ClusterRerouteRequestParameters, ClusterRerouteResponse, IClusterRerouteResponse>(
				clusterRerouteSelector,
				(p, d) => this.RawDispatch.ClusterRerouteDispatchAsync<ClusterRerouteResponse>(p, d)
			);
		}

		/// <inheritdoc />
		public IClusterRerouteResponse ClusterReroute(IClusterRerouteRequest clusterRerouteRequest)
		{
			return this.Dispatch<IClusterRerouteRequest, ClusterRerouteRequestParameters, ClusterRerouteResponse>(
				clusterRerouteRequest,
				(p, d) => this.RawDispatch.ClusterRerouteDispatch<ClusterRerouteResponse>(p, d)
			);
		}

		/// <inheritdoc />
		public Task<IClusterRerouteResponse> ClusterRerouteAsync(IClusterRerouteRequest clusterRerouteRequest)
		{
			return this.DispatchAsync<IClusterRerouteRequest, ClusterRerouteRequestParameters, ClusterRerouteResponse, IClusterRerouteResponse>(
				clusterRerouteRequest,
				(p, d) => this.RawDispatch.ClusterRerouteDispatchAsync<ClusterRerouteResponse>(p, d)
			);
		}
	}
}
