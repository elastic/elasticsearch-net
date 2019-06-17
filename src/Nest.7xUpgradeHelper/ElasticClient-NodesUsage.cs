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
		[Obsolete("Moved to client.Nodes.Usage(), please update this usage.")]
		public static NodesUsageResponse NodesUsage(this IElasticClient client, Func<NodesUsageDescriptor, INodesUsageRequest> selector = null)
			=> client.Nodes.Usage(selector);

		[Obsolete("Moved to client.Nodes.Usage(), please update this usage.")]
		public static NodesUsageResponse NodesUsage(this IElasticClient client, INodesUsageRequest request)
			=> client.Nodes.Usage(request);

		[Obsolete("Moved to client.Nodes.UsageAsync(), please update this usage.")]
		public static Task<NodesUsageResponse> NodesUsageAsync(this IElasticClient client,
			Func<NodesUsageDescriptor, INodesUsageRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Nodes.UsageAsync(selector, ct);

		[Obsolete("Moved to client.Nodes.UsageAsync(), please update this usage.")]
		public static Task<NodesUsageResponse> NodesUsageAsync(this IElasticClient client, INodesUsageRequest request, CancellationToken ct = default)
			=> client.Nodes.UsageAsync(request, ct);
	}
}
