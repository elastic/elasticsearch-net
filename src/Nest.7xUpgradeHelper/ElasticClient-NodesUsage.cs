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
		/// The cluster nodes usage API allows to retrieve information on the usage of features for each node.
		/// <para> </para>
		/// https://www.elastic.co/guide/en/elasticsearch/reference/master/cluster-nodes-usage.html
		/// </summary>
		/// <param name="selector">An optional descriptor to further describe the nodes usage operation</param>
		public static NodesUsageResponse NodesUsage(this IElasticClient client,Func<NodesUsageDescriptor, INodesUsageRequest> selector = null);

		/// <inheritdoc />
		public static NodesUsageResponse NodesUsage(this IElasticClient client,INodesUsageRequest request);

		/// <inheritdoc />
		public static Task<NodesUsageResponse> NodesUsageAsync(this IElasticClient client,Func<NodesUsageDescriptor, INodesUsageRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<NodesUsageResponse> NodesUsageAsync(this IElasticClient client,INodesUsageRequest request, CancellationToken ct = default);
	}

}
