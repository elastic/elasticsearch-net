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
		NodesStatsResponse NodesStats(Func<NodesStatsDescriptor, INodesStatsRequest> selector = null);

		/// <inheritdoc />
		NodesStatsResponse NodesStats(INodesStatsRequest request);

		/// <inheritdoc />
		Task<NodesStatsResponse> NodesStatsAsync(Func<NodesStatsDescriptor, INodesStatsRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<NodesStatsResponse> NodesStatsAsync(INodesStatsRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public NodesStatsResponse NodesStats(Func<NodesStatsDescriptor, INodesStatsRequest> selector = null) =>
			NodesStats(selector.InvokeOrDefault(new NodesStatsDescriptor()));

		/// <inheritdoc />
		public NodesStatsResponse NodesStats(INodesStatsRequest request) =>
			DoRequest<INodesStatsRequest, NodesStatsResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<NodesStatsResponse> NodesStatsAsync(
			Func<NodesStatsDescriptor, INodesStatsRequest> selector = null,
			CancellationToken ct = default
		) => NodesStatsAsync(selector.InvokeOrDefault(new NodesStatsDescriptor()), ct);

		/// <inheritdoc />
		public Task<NodesStatsResponse> NodesStatsAsync(INodesStatsRequest request, CancellationToken ct = default) =>
			DoRequestAsync<INodesStatsRequest, NodesStatsResponse, NodesStatsResponse>(request, request.RequestParameters, ct);
	}
}
