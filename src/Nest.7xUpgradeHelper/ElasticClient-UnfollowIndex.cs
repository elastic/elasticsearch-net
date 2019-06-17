using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.CrossClusterReplication.UnfollowIndex(), please update this usage.")]
		public static UnfollowIndexResponse UnfollowIndex(this IElasticClient client, IndexName index,
			Func<UnfollowIndexDescriptor, IUnfollowIndexRequest> selector = null
		)
			=> client.CrossClusterReplication.UnfollowIndex(index, selector);

		[Obsolete("Moved to client.CrossClusterReplication.UnfollowIndex(), please update this usage.")]
		public static UnfollowIndexResponse UnfollowIndex(this IElasticClient client, IUnfollowIndexRequest request)
			=> client.CrossClusterReplication.UnfollowIndex(request);

		[Obsolete("Moved to client.CrossClusterReplication.UnfollowIndexAsync(), please update this usage.")]
		public static Task<UnfollowIndexResponse> UnfollowIndexAsync(this IElasticClient client, IndexName index,
			Func<UnfollowIndexDescriptor, IUnfollowIndexRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.CrossClusterReplication.UnfollowIndexAsync(index, selector, ct);

		[Obsolete("Moved to client.CrossClusterReplication.UnfollowIndexAsync(), please update this usage.")]
		public static Task<UnfollowIndexResponse> UnfollowIndexAsync(this IElasticClient client, IUnfollowIndexRequest request,
			CancellationToken ct = default
		)
			=> client.CrossClusterReplication.UnfollowIndexAsync(request, ct);
	}
}
