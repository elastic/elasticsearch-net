using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
		public static NodesHotThreadsResponse NodesHotThreads(this IElasticClient client,Func<NodesHotThreadsDescriptor, INodesHotThreadsRequest> selector = null);

		/// <inheritdoc />
		public static NodesHotThreadsResponse NodesHotThreads(this IElasticClient client,INodesHotThreadsRequest request);

		/// <inheritdoc />
		public static Task<NodesHotThreadsResponse> NodesHotThreadsAsync(this IElasticClient client,Func<NodesHotThreadsDescriptor, INodesHotThreadsRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<NodesHotThreadsResponse> NodesHotThreadsAsync(this IElasticClient client,INodesHotThreadsRequest request, CancellationToken ct = default);
	}

}
