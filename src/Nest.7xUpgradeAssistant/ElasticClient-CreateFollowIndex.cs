using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.CrossClusterReplication.CreateFollowIndex(), please update this usage.")]
		public static CreateFollowIndexResponse CreateFollowIndex(this IElasticClient client, IndexName index,
			Func<CreateFollowIndexDescriptor, ICreateFollowIndexRequest> selector
		)
			=> client.CrossClusterReplication.CreateFollowIndex(index, selector);

		[Obsolete("Moved to client.CrossClusterReplication.CreateFollowIndex(), please update this usage.")]
		public static CreateFollowIndexResponse CreateFollowIndex(this IElasticClient client, ICreateFollowIndexRequest request)
			=> client.CrossClusterReplication.CreateFollowIndex(request);

		[Obsolete("Moved to client.CrossClusterReplication.CreateFollowIndexAsync(), please update this usage.")]
		public static Task<CreateFollowIndexResponse> CreateFollowIndexAsync(this IElasticClient client, IndexName index,
			Func<CreateFollowIndexDescriptor, ICreateFollowIndexRequest> selector,
			CancellationToken ct = default
		)
			=> client.CrossClusterReplication.CreateFollowIndexAsync(index, selector, ct);

		[Obsolete("Moved to client.CrossClusterReplication.CreateFollowIndexAsync(), please update this usage.")]
		public static Task<CreateFollowIndexResponse> CreateFollowIndexAsync(this IElasticClient client, ICreateFollowIndexRequest request,
			CancellationToken ct = default
		)
			=> client.CrossClusterReplication.CreateFollowIndexAsync(request, ct);
	}
}
