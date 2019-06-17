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
		/// An API allowing to get the current hot threads on each node in the cluster.
		/// </summary>
		/// <param name="selector"></param>
		/// <returns>An optional descriptor to further describe the nodes hot threads operation</returns>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static NodesHotThreadsResponse NodesHotThreads(this IElasticClient client,
			Func<NodesHotThreadsDescriptor, INodesHotThreadsRequest> selector = null
		)
			=> client.Nodes.HotThreads(selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static NodesHotThreadsResponse NodesHotThreads(this IElasticClient client, INodesHotThreadsRequest request)
			=> client.Nodes.HotThreads(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<NodesHotThreadsResponse> NodesHotThreadsAsync(this IElasticClient client,
			Func<NodesHotThreadsDescriptor, INodesHotThreadsRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Nodes.HotThreadsAsync(selector, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<NodesHotThreadsResponse> NodesHotThreadsAsync(this IElasticClient client, INodesHotThreadsRequest request,
			CancellationToken ct = default
		)
			=> client.Nodes.HotThreadsAsync(request, ct);
	}
}
