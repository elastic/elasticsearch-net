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
		/// The cluster nodes stats API allows to retrieve one or more (or all) of the cluster nodes statistics.
		/// <para> </para>
		/// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/cluster-nodes-stats.html
		/// </summary>
		/// <param name="selector">An optional descriptor to further describe the nodes stats operation</param>
		INodesStatsResponse NodesStats(Func<NodesStatsDescriptor, INodesStatsRequest> selector = null);

		/// <inheritdoc />
		INodesStatsResponse NodesStats(INodesStatsRequest request);

		/// <inheritdoc />
		Task<INodesStatsResponse> NodesStatsAsync(Func<NodesStatsDescriptor, INodesStatsRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<INodesStatsResponse> NodesStatsAsync(INodesStatsRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public INodesStatsResponse NodesStats(Func<NodesStatsDescriptor, INodesStatsRequest> selector = null) =>
			NodesStats(selector.InvokeOrDefault(new NodesStatsDescriptor()));

		/// <inheritdoc />
		public INodesStatsResponse NodesStats(INodesStatsRequest request) =>
			DoRequest<INodesStatsRequest, NodesStatsResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<INodesStatsResponse> NodesStatsAsync(
			Func<NodesStatsDescriptor, INodesStatsRequest> selector = null,
			CancellationToken ct = default
		) => NodesStatsAsync(selector.InvokeOrDefault(new NodesStatsDescriptor()), ct);

		/// <inheritdoc />
		public Task<INodesStatsResponse> NodesStatsAsync(INodesStatsRequest request, CancellationToken ct = default) =>
			DoRequestAsync<INodesStatsRequest, INodesStatsResponse, NodesStatsResponse>(request, request.RequestParameters, ct);
	}
}
