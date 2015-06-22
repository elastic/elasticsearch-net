using Elasticsearch.Net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Nest
{
	using NodesHotThreadConverter = Func<IElasticsearchResponse, Stream, NodesHotThreadsResponse>;

	public partial class ElasticClient
	{

		/// <inheritdoc />
		public INodeStatsResponse NodesStats(Func<NodesStatsDescriptor, NodesStatsDescriptor> selector = null)
		{
			selector = selector ?? (s => s);
			return this.Dispatcher.Dispatch<NodesStatsDescriptor, NodesStatsRequestParameters, NodeStatsResponse>(
				selector,
				(p, d) => this.RawDispatch.NodesStatsDispatch<NodeStatsResponse>(p)
			);
		}

		/// <inheritdoc />
		public INodeStatsResponse NodesStats(INodesStatsRequest nodesStatsRequest)
		{
			return this.Dispatcher.Dispatch<INodesStatsRequest, NodesStatsRequestParameters, NodeStatsResponse>(
				nodesStatsRequest,
				(p, d) => this.RawDispatch.NodesStatsDispatch<NodeStatsResponse>(p)
			);
		}

		/// <inheritdoc />
		public Task<INodeStatsResponse> NodesStatsAsync(Func<NodesStatsDescriptor, NodesStatsDescriptor> selector = null)
		{
			selector = selector ?? (s => s);
			return this.Dispatcher.DispatchAsync<NodesStatsDescriptor, NodesStatsRequestParameters, NodeStatsResponse, INodeStatsResponse>(
				selector,
				(p, d) => this.RawDispatch.NodesStatsDispatchAsync<NodeStatsResponse>(p)
			);
		}

		/// <inheritdoc />
		public Task<INodeStatsResponse> NodesStatsAsync(INodesStatsRequest nodesStatsRequest)
		{
			return this.Dispatcher.DispatchAsync<INodesStatsRequest, NodesStatsRequestParameters, NodeStatsResponse, INodeStatsResponse>(
				nodesStatsRequest,
				(p, d) => this.RawDispatch.NodesStatsDispatchAsync<NodeStatsResponse>(p)
			);
		}
	}
}