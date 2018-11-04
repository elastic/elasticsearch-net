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
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<INodesStatsResponse> NodesStatsAsync(INodesStatsRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public INodesStatsResponse NodesStats(Func<NodesStatsDescriptor, INodesStatsRequest> selector = null) =>
			NodesStats(selector.InvokeOrDefault(new NodesStatsDescriptor()));

		/// <inheritdoc />
		public INodesStatsResponse NodesStats(INodesStatsRequest request) =>
			Dispatcher.Dispatch<INodesStatsRequest, NodesStatsRequestParameters, NodesStatsResponse>(
				request,
				(p, d) => LowLevelDispatch.NodesStatsDispatch<NodesStatsResponse>(p)
			);

		/// <inheritdoc />
		public Task<INodesStatsResponse> NodesStatsAsync(Func<NodesStatsDescriptor, INodesStatsRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			NodesStatsAsync(selector.InvokeOrDefault(new NodesStatsDescriptor()), cancellationToken);

		/// <inheritdoc />
		public Task<INodesStatsResponse> NodesStatsAsync(INodesStatsRequest request, CancellationToken cancellationToken = default(CancellationToken)
		) =>
			Dispatcher.DispatchAsync<INodesStatsRequest, NodesStatsRequestParameters, NodesStatsResponse, INodesStatsResponse>(
				request,
				cancellationToken,
				(p, d, c) => LowLevelDispatch.NodesStatsDispatchAsync<NodesStatsResponse>(p, c)
			);
	}
}
