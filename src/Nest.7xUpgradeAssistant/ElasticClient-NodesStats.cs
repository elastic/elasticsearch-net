using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	using NodesHotThreadConverter = Func<IApiCallDetails, Stream, NodesHotThreadsResponse>;

	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Nodes.Stats(), please update this usage.")]
		public static NodesStatsResponse NodesStats(this IElasticClient client, Func<NodesStatsDescriptor, INodesStatsRequest> selector = null)
			=> client.Nodes.Stats(selector);

		[Obsolete("Moved to client.Nodes.Stats(), please update this usage.")]
		public static NodesStatsResponse NodesStats(this IElasticClient client, INodesStatsRequest request)
			=> client.Nodes.Stats(request);

		[Obsolete("Moved to client.Nodes.StatsAsync(), please update this usage.")]
		public static Task<NodesStatsResponse> NodesStatsAsync(this IElasticClient client,
			Func<NodesStatsDescriptor, INodesStatsRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Nodes.StatsAsync(selector, ct);

		[Obsolete("Moved to client.Nodes.StatsAsync(), please update this usage.")]
		public static Task<NodesStatsResponse> NodesStatsAsync(this IElasticClient client, INodesStatsRequest request, CancellationToken ct = default)
			=> client.Nodes.StatsAsync(request, ct);
	}
}
