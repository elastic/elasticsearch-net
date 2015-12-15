using System;
using System.IO;
using System.Threading.Tasks;
using Elasticsearch.Net;

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
		INodesInfoResponse NodesInfo(INodesInfoRequest request);

		/// <inheritdoc/>
		Task<INodesInfoResponse> NodesInfoAsync(Func<NodesInfoDescriptor, INodesInfoRequest> selector = null);

		/// <inheritdoc/>
		Task<INodesInfoResponse> NodesInfoAsync(INodesInfoRequest request);

	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public INodesInfoResponse NodesInfo(Func<NodesInfoDescriptor, INodesInfoRequest> selector = null) =>
			this.NodesInfo(selector.InvokeOrDefault(new NodesInfoDescriptor()));

		/// <inheritdoc/>
		public INodesInfoResponse NodesInfo(INodesInfoRequest request) => 
			this.Dispatcher.Dispatch<INodesInfoRequest, NodesInfoRequestParameters, NodesInfoResponse>(
				request,
				(p, d) => this.LowLevelDispatch.NodesInfoDispatch<NodesInfoResponse>(p)
			);

		/// <inheritdoc/>
		public Task<INodesInfoResponse> NodesInfoAsync(Func<NodesInfoDescriptor, INodesInfoRequest> selector = null) =>
			this.NodesInfoAsync(selector.InvokeOrDefault(new NodesInfoDescriptor()));

		/// <inheritdoc/>
		public Task<INodesInfoResponse> NodesInfoAsync(INodesInfoRequest request) => 
			this.Dispatcher.DispatchAsync<INodesInfoRequest, NodesInfoRequestParameters, NodesInfoResponse, INodesInfoResponse>(
				request,
				(p, d) => this.LowLevelDispatch.NodesInfoDispatchAsync<NodesInfoResponse>(p)
			);
	}
}