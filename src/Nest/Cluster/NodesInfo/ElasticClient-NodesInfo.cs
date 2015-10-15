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
		INodesInfoResponse NodesInfo(Func<NodesInfoDescriptor, INodesInfoRequest> selector = null);

		/// <inheritdoc/>
		INodesInfoResponse NodesInfo(INodesInfoRequest nodesInfoRequest);

		/// <inheritdoc/>
		Task<INodesInfoResponse> NodesInfoAsync(Func<NodesInfoDescriptor, INodesInfoRequest> selector = null);

		/// <inheritdoc/>
		Task<INodesInfoResponse> NodesInfoAsync(INodesInfoRequest nodesInfoRequest);

	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public INodesInfoResponse NodesInfo(Func<NodesInfoDescriptor, INodesInfoRequest> selector = null) =>
			this.NodesInfo(selector.InvokeOrDefault(new NodesInfoDescriptor()));

		/// <inheritdoc/>
		public INodesInfoResponse NodesInfo(INodesInfoRequest nodesInfoRequest) => 
			this.Dispatcher.Dispatch<INodesInfoRequest, NodesInfoRequestParameters, NodesInfoResponse>(
				nodesInfoRequest,
				(p, d) => this.LowLevelDispatch.NodesInfoDispatch<NodesInfoResponse>(p)
			);

		/// <inheritdoc/>
		public Task<INodesInfoResponse> NodesInfoAsync(Func<NodesInfoDescriptor, INodesInfoRequest> selector = null) =>
			this.NodesInfoAsync(selector.InvokeOrDefault(new NodesInfoDescriptor()));

		/// <inheritdoc/>
		public Task<INodesInfoResponse> NodesInfoAsync(INodesInfoRequest nodesInfoRequest) => 
			this.Dispatcher.DispatchAsync<INodesInfoRequest, NodesInfoRequestParameters, NodesInfoResponse, INodesInfoResponse>(
				nodesInfoRequest,
				(p, d) => this.LowLevelDispatch.NodesInfoDispatchAsync<NodesInfoResponse>(p)
			);
	}
}