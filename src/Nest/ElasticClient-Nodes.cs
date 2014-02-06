using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Nest
{
	public partial class ElasticClient
	{
		public INodeInfoResponse ClusterNodeInfo(Func<ClusterNodeInfoDescriptor, ClusterNodeInfoDescriptor> selector=null)
		{
			selector = selector ?? (s => s);
			return this.Dispatch<ClusterNodeInfoDescriptor, ClusterNodeInfoQueryString, NodeInfoResponse>(
				selector,
				(p, d)=> this.RawDispatch.ClusterNodeInfoDispatch(p)
			);
		}

		public Task<INodeInfoResponse> ClusterNodeInfoAsync(Func<ClusterNodeInfoDescriptor, ClusterNodeInfoDescriptor> selector = null)
		{
			selector = selector ?? (s => s);
			return this.DispatchAsync<ClusterNodeInfoDescriptor, ClusterNodeInfoQueryString, NodeInfoResponse, INodeInfoResponse>(
				selector,
				(p, d)=> this.RawDispatch.ClusterNodeInfoDispatchAsync(p)
			);
		}

		/// <summary>
		/// The cluster nodes stats API allows to retrieve one or more (or all) of the cluster nodes statistics.
		///<pre>http://elasticsearch.org/guide/reference/api/admin-cluster-nodes-stats/</pre>	
		/// </summary>
		/// <param name="selector">limit the results on stats type or indices/nodes, defaults to all types and all indices/nodes</param>
		public INodeStatsResponse ClusterNodeStats(Func<ClusterNodeStatsDescriptor, ClusterNodeStatsDescriptor> selector = null)
		{
			selector = selector ?? (s => s.All());
			return this.Dispatch<ClusterNodeStatsDescriptor, ClusterNodeStatsQueryString, NodeStatsResponse>(
				selector,
				(p, d) => this.RawDispatch.ClusterNodeStatsDispatch(p)
			);
		}
		
		/// <summary>
		/// The cluster nodes stats API allows to retrieve one or more (or all) of the cluster nodes statistics.
		///<pre>http://elasticsearch.org/guide/reference/api/admin-cluster-nodes-stats/</pre>	
		/// </summary>
		/// <param name="selector">limit the results on stats type or indices/nodes, defaults to all types and all indices/nodes</param>
		public Task<INodeStatsResponse> ClusterNodeStatsAsync(Func<ClusterNodeStatsDescriptor, ClusterNodeStatsDescriptor> selector = null)
		{
			selector = selector ?? (s => s.All());
			return this.DispatchAsync<ClusterNodeStatsDescriptor, ClusterNodeStatsQueryString, NodeStatsResponse, INodeStatsResponse>(
				selector,
				(p, d) => this.RawDispatch.ClusterNodeStatsDispatchAsync(p)
			);
		}
	}
}
