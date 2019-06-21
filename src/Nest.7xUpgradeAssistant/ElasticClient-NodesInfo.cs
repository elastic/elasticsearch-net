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
		[Obsolete("Moved to client.Nodes.Info(), please update this usage.")]
		public static NodesInfoResponse NodesInfo(this IElasticClient client, Func<NodesInfoDescriptor, INodesInfoRequest> selector = null)
			=> client.Nodes.Info(selector);

		[Obsolete("Moved to client.Nodes.Info(), please update this usage.")]
		public static NodesInfoResponse NodesInfo(this IElasticClient client, INodesInfoRequest request)
			=> client.Nodes.Info(request);

		[Obsolete("Moved to client.Nodes.InfoAsync(), please update this usage.")]
		public static Task<NodesInfoResponse> NodesInfoAsync(this IElasticClient client, Func<NodesInfoDescriptor, INodesInfoRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Nodes.InfoAsync(selector, ct);

		[Obsolete("Moved to client.Nodes.InfoAsync(), please update this usage.")]
		public static Task<NodesInfoResponse> NodesInfoAsync(this IElasticClient client, INodesInfoRequest request, CancellationToken ct = default)
			=> client.Nodes.InfoAsync(request, ct);
	}
}
