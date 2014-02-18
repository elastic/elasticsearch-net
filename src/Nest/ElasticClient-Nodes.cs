using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Nest
{
	public partial class ElasticClient
	{
		public INodeInfoResponse NodesInfo(Func<NodesInfoDescriptor, NodesInfoDescriptor> selector=null)
		{
			selector = selector ?? (s => s);
			return this.Dispatch<NodesInfoDescriptor, NodesInfoQueryString, NodeInfoResponse>(
				selector,
				(p, d)=> this.RawDispatch.NodesInfoDispatch(p)
			);
		}

		public Task<INodeInfoResponse> NodesInfoAsync(Func<NodesInfoDescriptor, NodesInfoDescriptor> selector = null)
		{
			selector = selector ?? (s => s);
			return this.DispatchAsync<NodesInfoDescriptor, NodesInfoQueryString, NodeInfoResponse, INodeInfoResponse>(
				selector,
				(p, d)=> this.RawDispatch.NodesInfoDispatchAsync(p)
			);
		}

		/// <summary>
		/// The cluster nodes stats API allows to retrieve one or more (or all) of the cluster nodes statistics.
		///<pre>http://elasticsearch.org/guide/reference/api/admin-cluster-nodes-stats/</pre>	
		/// </summary>
		/// <param name="selector">limit the results on stats type or indices/nodes, defaults to all types and all indices/nodes</param>
		public INodeStatsResponse NodesStats(Func<NodesStatsDescriptor, NodesStatsDescriptor> selector = null)
		{
			selector = selector ?? (s => s);
			return this.Dispatch<NodesStatsDescriptor, NodesStatsQueryString, NodeStatsResponse>(
				selector,
				(p, d) => this.RawDispatch.NodesStatsDispatch(p)
			);
		}
		
		/// <summary>
		/// The cluster nodes stats API allows to retrieve one or more (or all) of the cluster nodes statistics.
		///<pre>http://elasticsearch.org/guide/reference/api/admin-cluster-nodes-stats/</pre>	
		/// </summary>
		/// <param name="selector">limit the results on stats type or indices/nodes, defaults to all types and all indices/nodes</param>
		public Task<INodeStatsResponse> NodesStatsAsync(Func<NodesStatsDescriptor, NodesStatsDescriptor> selector = null)
		{
			selector = selector ?? (s => s);
			return this.DispatchAsync<NodesStatsDescriptor, NodesStatsQueryString, NodeStatsResponse, INodeStatsResponse>(
				selector,
				(p, d) => this.RawDispatch.NodesStatsDispatchAsync(p)
			);
		}
	}
}
