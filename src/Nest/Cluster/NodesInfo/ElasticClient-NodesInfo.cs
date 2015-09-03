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
	using NodesHotThreadConverter = Func<IApiCallDetails, Stream, NodesHotThreadsResponse>;

	public partial interface IElasticClient
	{
		/// <summary>
		/// The cluster nodes info API allows to retrieve one or more (or all) of the cluster nodes information.
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/cluster-nodes-info.html
		/// </summary>
		/// <param name="selector">An optional descriptor to further describe the nodes info operation</param>
		INodeInfoResponse NodesInfo(Func<NodesInfoDescriptor, INodesInfoRequest> selector = null);

		/// <inheritdoc/>
		INodeInfoResponse NodesInfo(INodesInfoRequest nodesInfoRequest);

		/// <inheritdoc/>
		Task<INodeInfoResponse> NodesInfoAsync(Func<NodesInfoDescriptor, INodesInfoRequest> selector = null);

		/// <inheritdoc/>
		Task<INodeInfoResponse> NodesInfoAsync(INodesInfoRequest nodesInfoRequest);

	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public INodeInfoResponse NodesInfo(Func<NodesInfoDescriptor, INodesInfoRequest> selector = null) =>
			this.NodesInfo(selector.InvokeOrDefault(new NodesInfoDescriptor()));

		/// <inheritdoc/>
		public INodeInfoResponse NodesInfo(INodesInfoRequest nodesInfoRequest) => 
			this.Dispatcher.Dispatch<INodesInfoRequest, NodesInfoRequestParameters, NodeInfoResponse>(
				nodesInfoRequest,
				(p, d) => this.LowLevelDispatch.NodesInfoDispatch<NodeInfoResponse>(p)
			);

		/// <inheritdoc/>
		public Task<INodeInfoResponse> NodesInfoAsync(Func<NodesInfoDescriptor, INodesInfoRequest> selector = null) =>
			this.NodesInfoAsync(selector.InvokeOrDefault(new NodesInfoDescriptor()));

		/// <inheritdoc/>
		public Task<INodeInfoResponse> NodesInfoAsync(INodesInfoRequest nodesInfoRequest) => 
			this.Dispatcher.DispatchAsync<INodesInfoRequest, NodesInfoRequestParameters, NodeInfoResponse, INodeInfoResponse>(
				nodesInfoRequest,
				(p, d) => this.LowLevelDispatch.NodesInfoDispatchAsync<NodeInfoResponse>(p)
			);
	}
}