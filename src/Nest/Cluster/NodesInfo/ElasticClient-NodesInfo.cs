using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	using NodesHotThreadConverter = Func<IApiCallDetails, Stream, NodesHotThreadsResponse>;

	public partial interface IElasticClient
	{
		/// <summary>
		/// The cluster nodes info API allows to retrieve one or more (or all) of the cluster nodes information.
		/// <para> </para>
		/// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/cluster-nodes-info.html
		/// </summary>
		/// <param name="selector">An optional descriptor to further describe the nodes info operation</param>
		INodesInfoResponse NodesInfo(Func<NodesInfoDescriptor, INodesInfoRequest> selector = null);

		/// <inheritdoc />
		INodesInfoResponse NodesInfo(INodesInfoRequest request);

		/// <inheritdoc />
		Task<INodesInfoResponse> NodesInfoAsync(Func<NodesInfoDescriptor, INodesInfoRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<INodesInfoResponse> NodesInfoAsync(INodesInfoRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public INodesInfoResponse NodesInfo(Func<NodesInfoDescriptor, INodesInfoRequest> selector = null) =>
			NodesInfo(selector.InvokeOrDefault(new NodesInfoDescriptor()));

		/// <inheritdoc />
		public INodesInfoResponse NodesInfo(INodesInfoRequest request) =>
			Dispatch2<INodesInfoRequest, NodesInfoResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<INodesInfoResponse> NodesInfoAsync(Func<NodesInfoDescriptor, INodesInfoRequest> selector = null,
			CancellationToken ct = default
		) =>
			NodesInfoAsync(selector.InvokeOrDefault(new NodesInfoDescriptor()), ct);

		/// <inheritdoc />
		public Task<INodesInfoResponse> NodesInfoAsync(INodesInfoRequest request, CancellationToken ct = default) =>
			Dispatch2Async<INodesInfoRequest, INodesInfoResponse, NodesInfoResponse>(request, request.RequestParameters, ct);
	}
}
