using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Nest
{
	public partial class ElasticClient
	{
		public INodeInfoResponse ClusterNodeInfo(Func<ClusterNodeInfoDescriptor, ClusterNodeInfoDescriptor> selector)
		{
			return this.Dispatch<ClusterNodeInfoDescriptor, ClusterNodeInfoQueryString, NodeInfoResponse>(
				selector,
				(p, d)=> this.RawDispatch.ClusterNodeInfoDispatch(p)
			);
		}

		public Task<INodeInfoResponse> ClusterNodeInfoAsync(Func<ClusterNodeInfoDescriptor, ClusterNodeInfoDescriptor> selector)
		{
			return this.DispatchAsync<ClusterNodeInfoDescriptor, ClusterNodeInfoQueryString, NodeInfoResponse, INodeInfoResponse>(
				selector,
				(p, d)=> this.RawDispatch.ClusterNodeInfoDispatchAsync(p)
			);
		}

		public INodeStatsResponse ClusterNodeStats(Func<ClusterNodeStatsDescriptor, ClusterNodeStatsDescriptor> selector)
		{
			return this.Dispatch<ClusterNodeStatsDescriptor, ClusterNodeStatsQueryString, NodeStatsResponse>(
				selector,
				(p, d) => this.RawDispatch.ClusterNodeStatsDispatch(p)
			);
		}
		public Task<INodeStatsResponse> ClusterNodeStatsAsync(Func<ClusterNodeStatsDescriptor, ClusterNodeStatsDescriptor> selector)
		{
			return this.DispatchAsync<ClusterNodeStatsDescriptor, ClusterNodeStatsQueryString, NodeStatsResponse, INodeStatsResponse>(
				selector,
				(p, d) => this.RawDispatch.ClusterNodeStatsDispatchAsync(p)
			);
		}
	}
}
