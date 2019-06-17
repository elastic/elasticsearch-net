using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Stops the following task associated with a follower index and removes index metadata and settings associated with
		/// cross-cluster replication. This enables the index to treated as a regular index. The follower index must be paused and
		/// closed
		/// before invoking the unfollow API.
		/// </summary>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static UnfollowIndexResponse UnfollowIndex(this IElasticClient client, IndexName index,
			Func<UnfollowIndexDescriptor, IUnfollowIndexRequest> selector = null
		)
			=> client.CrossClusterReplication.UnfollowIndex(index, selector);

		/// <inheritdoc cref="UnfollowIndex(IndexName, System.Func{Nest.UnfollowIndexDescriptor,Nest.IUnfollowIndexRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static UnfollowIndexResponse UnfollowIndex(this IElasticClient client, IUnfollowIndexRequest request)
			=> client.CrossClusterReplication.UnfollowIndex(request);

		/// <inheritdoc cref="UnfollowIndex(IndexName, System.Func{Nest.UnfollowIndexDescriptor,Nest.IUnfollowIndexRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<UnfollowIndexResponse> UnfollowIndexAsync(this IElasticClient client, IndexName index,
			Func<UnfollowIndexDescriptor, IUnfollowIndexRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.CrossClusterReplication.UnfollowIndexAsync(index, selector, ct);

		/// <inheritdoc cref="UnfollowIndex(IndexName, System.Func{Nest.UnfollowIndexDescriptor,Nest.IUnfollowIndexRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<UnfollowIndexResponse> UnfollowIndexAsync(this IElasticClient client, IUnfollowIndexRequest request,
			CancellationToken ct = default
		)
			=> client.CrossClusterReplication.UnfollowIndexAsync(request, ct);
	}
}
