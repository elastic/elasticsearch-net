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
		[Obsolete("Moved to client.Nodes.HotThreads(), please update this usage.")]
		public static NodesHotThreadsResponse NodesHotThreads(this IElasticClient client,
			Func<NodesHotThreadsDescriptor, INodesHotThreadsRequest> selector = null
		)
			=> client.Nodes.HotThreads(selector);

		[Obsolete("Moved to client.Nodes.HotThreads(), please update this usage.")]
		public static NodesHotThreadsResponse NodesHotThreads(this IElasticClient client, INodesHotThreadsRequest request)
			=> client.Nodes.HotThreads(request);

		[Obsolete("Moved to client.Nodes.HotThreadsAsync(), please update this usage.")]
		public static Task<NodesHotThreadsResponse> NodesHotThreadsAsync(this IElasticClient client,
			Func<NodesHotThreadsDescriptor, INodesHotThreadsRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Nodes.HotThreadsAsync(selector, ct);

		[Obsolete("Moved to client.Nodes.HotThreadsAsync(), please update this usage.")]
		public static Task<NodesHotThreadsResponse> NodesHotThreadsAsync(this IElasticClient client, INodesHotThreadsRequest request,
			CancellationToken ct = default
		)
			=> client.Nodes.HotThreadsAsync(request, ct);
	}
}
