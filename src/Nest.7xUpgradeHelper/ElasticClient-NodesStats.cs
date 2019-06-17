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
		/// <summary>
		/// The cluster nodes stats API allows to retrieve one or more (or all) of the cluster nodes statistics.
		/// <para> </para>
		/// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/cluster-nodes-stats.html
		/// </summary>
		/// <param name="selector">An optional descriptor to further describe the nodes stats operation</param>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static NodesStatsResponse NodesStats(this IElasticClient client, Func<NodesStatsDescriptor, INodesStatsRequest> selector = null)
			=> client.Nodes.Stats(selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static NodesStatsResponse NodesStats(this IElasticClient client, INodesStatsRequest request)
			=> client.Nodes.Stats(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<NodesStatsResponse> NodesStatsAsync(this IElasticClient client,
			Func<NodesStatsDescriptor, INodesStatsRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Nodes.StatsAsync(selector, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<NodesStatsResponse> NodesStatsAsync(this IElasticClient client, INodesStatsRequest request, CancellationToken ct = default)
			=> client.Nodes.StatsAsync(request, ct);
	}
}
