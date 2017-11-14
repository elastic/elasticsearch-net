using System;
using System.IO;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	using System.Threading;
	using NodesHotThreadConverter = Func<IApiCallDetails, Stream, NodesHotThreadsResponse>;

	public partial interface IElasticClient
	{
		/// <summary>
		/// The cluster nodes usage API allows to retrieve information on the usage of features for each node.
		/// <para> </para>https://www.elastic.co/guide/en/elasticsearch/reference/master/cluster-nodes-usage.html
		/// </summary>
		/// <param name="selector">An optional descriptor to further describe the nodes usage operation</param>
		INodesUsageResponse NodesUsage(Func<NodesUsageDescriptor, INodesUsageRequest> selector = null);

		/// <inheritdoc/>
		INodesUsageResponse NodesUsage(INodesUsageRequest request);

		/// <inheritdoc/>
		Task<INodesUsageResponse> NodesUsageAsync(Func<NodesUsageDescriptor, INodesUsageRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken));

		/// <inheritdoc/>
		Task<INodesUsageResponse> NodesUsageAsync(INodesUsageRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public INodesUsageResponse NodesUsage(Func<NodesUsageDescriptor, INodesUsageRequest> selector = null) =>
			this.NodesUsage(selector.InvokeOrDefault(new NodesUsageDescriptor()));

		/// <inheritdoc/>
		public INodesUsageResponse NodesUsage(INodesUsageRequest request) =>
			this.Dispatcher.Dispatch<INodesUsageRequest, NodesUsageRequestParameters, NodesUsageResponse>(
				request,
				(p, d) => this.LowLevelDispatch.NodesUsageDispatch<NodesUsageResponse>(p)
			);

		/// <inheritdoc/>
		public Task<INodesUsageResponse> NodesUsageAsync(Func<NodesUsageDescriptor, INodesUsageRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.NodesUsageAsync(selector.InvokeOrDefault(new NodesUsageDescriptor()), cancellationToken);

		/// <inheritdoc/>
		public Task<INodesUsageResponse> NodesUsageAsync(INodesUsageRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<INodesUsageRequest, NodesUsageRequestParameters, NodesUsageResponse, INodesUsageResponse>(
				request,
				cancellationToken,
				(p, d, c) => this.LowLevelDispatch.NodesUsageDispatchAsync<NodesUsageResponse>(p, c)
			);
	}
}
