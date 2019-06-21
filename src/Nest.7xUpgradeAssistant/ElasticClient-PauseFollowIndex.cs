using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.CrossClusterReplication.PauseFollowIndex(), please update this usage.")]
		public static PauseFollowIndexResponse PauseFollowIndex(this IElasticClient client, IndexName index,
			Func<PauseFollowIndexDescriptor, IPauseFollowIndexRequest> selector = null
		)
			=> client.CrossClusterReplication.PauseFollowIndex(index, selector);

		[Obsolete("Moved to client.CrossClusterReplication.PauseFollowIndex(), please update this usage.")]
		public static PauseFollowIndexResponse PauseFollowIndex(this IElasticClient client, IPauseFollowIndexRequest request)
			=> client.CrossClusterReplication.PauseFollowIndex(request);

		[Obsolete("Moved to client.CrossClusterReplication.PauseFollowIndexAsync(), please update this usage.")]
		public static Task<PauseFollowIndexResponse> PauseFollowIndexAsync(this IElasticClient client, IndexName index,
			Func<PauseFollowIndexDescriptor, IPauseFollowIndexRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.CrossClusterReplication.PauseFollowIndexAsync(index, selector, ct);

		[Obsolete("Moved to client.CrossClusterReplication.PauseFollowIndexAsync(), please update this usage.")]
		public static Task<PauseFollowIndexResponse> PauseFollowIndexAsync(this IElasticClient client, IPauseFollowIndexRequest request,
			CancellationToken ct = default
		)
			=> client.CrossClusterReplication.PauseFollowIndexAsync(request, ct);
	}
}
