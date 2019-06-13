using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Does a request to the root of an elasticsearch node
		/// </summary>
		/// <param name="selector">A descriptor to further describe the root operation</param>
		public static RootNodeInfoResponse RootNodeInfo(this IElasticClient client,Func<RootNodeInfoDescriptor, IRootNodeInfoRequest> selector = null);

		/// <inheritdoc />
		public static RootNodeInfoResponse RootNodeInfo(this IElasticClient client,IRootNodeInfoRequest request);

		/// <inheritdoc />
		public static Task<RootNodeInfoResponse> RootNodeInfoAsync(this IElasticClient client,Func<RootNodeInfoDescriptor, IRootNodeInfoRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<RootNodeInfoResponse> RootNodeInfoAsync(this IElasticClient client,IRootNodeInfoRequest request, CancellationToken ct = default);
	}

}
