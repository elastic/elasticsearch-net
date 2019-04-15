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
		/// The cluster nodes usage API allows to retrieve information on the usage of features for each node.
		/// <para> </para>
		/// https://www.elastic.co/guide/en/elasticsearch/reference/master/cluster-nodes-usage.html
		/// </summary>
		/// <param name="selector">An optional descriptor to further describe the nodes usage operation</param>
		NodesUsageResponse NodesUsage(Func<NodesUsageDescriptor, INodesUsageRequest> selector = null);

		/// <inheritdoc />
		NodesUsageResponse NodesUsage(INodesUsageRequest request);

		/// <inheritdoc />
		Task<NodesUsageResponse> NodesUsageAsync(Func<NodesUsageDescriptor, INodesUsageRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<NodesUsageResponse> NodesUsageAsync(INodesUsageRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public NodesUsageResponse NodesUsage(Func<NodesUsageDescriptor, INodesUsageRequest> selector = null) =>
			NodesUsage(selector.InvokeOrDefault(new NodesUsageDescriptor()));

		/// <inheritdoc />
		public NodesUsageResponse NodesUsage(INodesUsageRequest request) =>
			DoRequest<INodesUsageRequest, NodesUsageResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<NodesUsageResponse> NodesUsageAsync(
			Func<NodesUsageDescriptor, INodesUsageRequest> selector = null,
			CancellationToken ct = default
		) =>
			NodesUsageAsync(selector.InvokeOrDefault(new NodesUsageDescriptor()), ct);

		/// <inheritdoc />
		public Task<NodesUsageResponse> NodesUsageAsync(INodesUsageRequest request, CancellationToken ct = default) =>
			DoRequestAsync<INodesUsageRequest, NodesUsageResponse, NodesUsageResponse>(request, request.RequestParameters, ct);
	}
}
