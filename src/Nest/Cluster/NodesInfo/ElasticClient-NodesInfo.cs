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
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<INodesInfoResponse> NodesInfoAsync(INodesInfoRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public INodesInfoResponse NodesInfo(Func<NodesInfoDescriptor, INodesInfoRequest> selector = null) =>
			NodesInfo(selector.InvokeOrDefault(new NodesInfoDescriptor()));

		/// <inheritdoc />
		public INodesInfoResponse NodesInfo(INodesInfoRequest request) =>
			Dispatcher.Dispatch<INodesInfoRequest, NodesInfoRequestParameters, NodesInfoResponse>(
				request,
				(p, d) => LowLevelDispatch.NodesInfoDispatch<NodesInfoResponse>(p)
			);

		/// <inheritdoc />
		public Task<INodesInfoResponse> NodesInfoAsync(Func<NodesInfoDescriptor, INodesInfoRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			NodesInfoAsync(selector.InvokeOrDefault(new NodesInfoDescriptor()), cancellationToken);

		/// <inheritdoc />
		public Task<INodesInfoResponse> NodesInfoAsync(INodesInfoRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			Dispatcher.DispatchAsync<INodesInfoRequest, NodesInfoRequestParameters, NodesInfoResponse, INodesInfoResponse>(
				request,
				cancellationToken,
				(p, d, c) => LowLevelDispatch.NodesInfoDispatchAsync<NodesInfoResponse>(p, c)
			);
	}
}
