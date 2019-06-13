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
		/// The cluster nodes info API allows to retrieve one or more (or all) of the cluster nodes information.
		/// <para> </para>
		/// http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/cluster-nodes-info.html
		/// </summary>
		/// <param name="selector">An optional descriptor to further describe the nodes info operation</param>
		public static NodesInfoResponse NodesInfo(this IElasticClient client,Func<NodesInfoDescriptor, INodesInfoRequest> selector = null);

		/// <inheritdoc />
		public static NodesInfoResponse NodesInfo(this IElasticClient client,INodesInfoRequest request);

		/// <inheritdoc />
		public static Task<NodesInfoResponse> NodesInfoAsync(this IElasticClient client,Func<NodesInfoDescriptor, INodesInfoRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<NodesInfoResponse> NodesInfoAsync(this IElasticClient client,INodesInfoRequest request, CancellationToken ct = default);
	}

}
