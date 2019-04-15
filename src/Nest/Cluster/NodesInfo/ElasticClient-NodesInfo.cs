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
		NodesInfoResponse NodesInfo(Func<NodesInfoDescriptor, INodesInfoRequest> selector = null);

		/// <inheritdoc />
		NodesInfoResponse NodesInfo(INodesInfoRequest request);

		/// <inheritdoc />
		Task<NodesInfoResponse> NodesInfoAsync(Func<NodesInfoDescriptor, INodesInfoRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<NodesInfoResponse> NodesInfoAsync(INodesInfoRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public NodesInfoResponse NodesInfo(Func<NodesInfoDescriptor, INodesInfoRequest> selector = null) =>
			NodesInfo(selector.InvokeOrDefault(new NodesInfoDescriptor()));

		/// <inheritdoc />
		public NodesInfoResponse NodesInfo(INodesInfoRequest request) =>
			DoRequest<INodesInfoRequest, NodesInfoResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<NodesInfoResponse> NodesInfoAsync(Func<NodesInfoDescriptor, INodesInfoRequest> selector = null,
			CancellationToken ct = default
		) =>
			NodesInfoAsync(selector.InvokeOrDefault(new NodesInfoDescriptor()), ct);

		/// <inheritdoc />
		public Task<NodesInfoResponse> NodesInfoAsync(INodesInfoRequest request, CancellationToken ct = default) =>
			DoRequestAsync<INodesInfoRequest, NodesInfoResponse, NodesInfoResponse>(request, request.RequestParameters, ct);
	}
}
