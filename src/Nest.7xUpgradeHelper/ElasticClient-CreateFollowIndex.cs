using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Creates a new follower index that is configured to follow the referenced leader index.
		/// When this API returns, the follower index exists, and cross-cluster replication starts replicating
		/// operations from the leader index to the follower index.
		/// </summary>
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static CreateFollowIndexResponse CreateFollowIndex(this IElasticClient client, IndexName index,
			Func<CreateFollowIndexDescriptor, ICreateFollowIndexRequest> selector
		)
			=> client.CrossClusterReplication.CreateFollowIndex(index, selector);

		/// <inheritdoc
		///     cref="CreateFollowIndex(IndexName, System.Func{Nest.CreateFollowIndexDescriptor,Nest.ICreateFollowIndexRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static CreateFollowIndexResponse CreateFollowIndex(this IElasticClient client, ICreateFollowIndexRequest request)
			=> client.CrossClusterReplication.CreateFollowIndex(request);

		/// <inheritdoc
		///     cref="CreateFollowIndex(IndexName, System.Func{Nest.CreateFollowIndexDescriptor,Nest.ICreateFollowIndexRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<CreateFollowIndexResponse> CreateFollowIndexAsync(this IElasticClient client, IndexName index,
			Func<CreateFollowIndexDescriptor, ICreateFollowIndexRequest> selector,
			CancellationToken ct = default
		)
			=> client.CrossClusterReplication.CreateFollowIndexAsync(index, selector, ct);

		/// <inheritdoc
		///     cref="CreateFollowIndex(IndexName, System.Func{Nest.CreateFollowIndexDescriptor,Nest.ICreateFollowIndexRequest})" />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<CreateFollowIndexResponse> CreateFollowIndexAsync(this IElasticClient client, ICreateFollowIndexRequest request,
			CancellationToken ct = default
		)
			=> client.CrossClusterReplication.CreateFollowIndexAsync(request, ct);
	}
}
