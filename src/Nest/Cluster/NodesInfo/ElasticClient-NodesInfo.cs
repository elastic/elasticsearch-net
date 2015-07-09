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
		public INodeInfoResponse NodesInfo(Func<NodesInfoDescriptor, NodesInfoDescriptor> selector = null)
		{
			selector = selector ?? (s => s);
			return this.Dispatcher.Dispatch<NodesInfoDescriptor, NodesInfoRequestParameters, NodeInfoResponse>(
				selector,
				(p, d) => this.LowLevelDispatch.NodesInfoDispatch<NodeInfoResponse>(p)
			);
		}

		/// <inheritdoc />
		public INodeInfoResponse NodesInfo(INodesInfoRequest nodesInfoRequest)
		{
			return this.Dispatcher.Dispatch<INodesInfoRequest, NodesInfoRequestParameters, NodeInfoResponse>(
				nodesInfoRequest,
				(p, d) => this.LowLevelDispatch.NodesInfoDispatch<NodeInfoResponse>(p)
			);
		}

		/// <inheritdoc />
		public Task<INodeInfoResponse> NodesInfoAsync(Func<NodesInfoDescriptor, NodesInfoDescriptor> selector = null)
		{
			selector = selector ?? (s => s);
			return this.Dispatcher.DispatchAsync<NodesInfoDescriptor, NodesInfoRequestParameters, NodeInfoResponse, INodeInfoResponse>(
				selector,
				(p, d) => this.LowLevelDispatch.NodesInfoDispatchAsync<NodeInfoResponse>(p)
			);
		}

		/// <inheritdoc />
		public Task<INodeInfoResponse> NodesInfoAsync(INodesInfoRequest nodesInfoRequest)
		{
			return this.Dispatcher.DispatchAsync<INodesInfoRequest, NodesInfoRequestParameters, NodeInfoResponse, INodeInfoResponse>(
				nodesInfoRequest,
				(p, d) => this.LowLevelDispatch.NodesInfoDispatchAsync<NodeInfoResponse>(p)
			);
		}
	}
}