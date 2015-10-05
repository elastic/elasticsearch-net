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
		/// The cluster nodes stats API allows to retrieve one or more (or all) of the cluster nodes statistics.
		/// <para> </para>http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/cluster-nodes-stats.html
		/// </summary>
		/// <param name="selector">An optional descriptor to further describe the nodes stats operation</param>
		INodesStatsResponse NodesStats(Func<NodesStatsDescriptor, INodesStatsRequest> selector = null);

		/// <inheritdoc/>
		INodesStatsResponse NodesStats(INodesStatsRequest nodesStatsRequest);

		/// <inheritdoc/>
		Task<INodesStatsResponse> NodesStatsAsync(Func<NodesStatsDescriptor, INodesStatsRequest> selector = null);

		/// <inheritdoc/>
		Task<INodesStatsResponse> NodesStatsAsync(INodesStatsRequest nodesStatsRequest);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public INodesStatsResponse NodesStats(Func<NodesStatsDescriptor, INodesStatsRequest> selector = null) =>
			this.NodesStats(selector.InvokeOrDefault(new NodesStatsDescriptor()));

		/// <inheritdoc/>
		public INodesStatsResponse NodesStats(INodesStatsRequest nodesStatsRequest) => 
			this.Dispatcher.Dispatch<INodesStatsRequest, NodesStatsRequestParameters, NodesStatsRsponse>(
				nodesStatsRequest,
				(p, d) => this.LowLevelDispatch.NodesStatsDispatch<NodesStatsRsponse>(p)
			);

		/// <inheritdoc/>
		public Task<INodesStatsResponse> NodesStatsAsync(Func<NodesStatsDescriptor, INodesStatsRequest> selector = null) =>
			this.NodesStatsAsync(selector.InvokeOrDefault(new NodesStatsDescriptor()));

		/// <inheritdoc/>
		public Task<INodesStatsResponse> NodesStatsAsync(INodesStatsRequest nodesStatsRequest) => 
			this.Dispatcher.DispatchAsync<INodesStatsRequest, NodesStatsRequestParameters, NodesStatsRsponse, INodesStatsResponse>(
				nodesStatsRequest,
				(p, d) => this.LowLevelDispatch.NodesStatsDispatchAsync<NodesStatsRsponse>(p)
			);
	}
}