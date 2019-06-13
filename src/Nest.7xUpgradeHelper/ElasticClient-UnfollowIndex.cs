using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Stops the following task associated with a follower index and removes index metadata and settings associated with
		/// cross-cluster replication. This enables the index to treated as a regular index. The follower index must be paused and closed
		/// before invoking the unfollow API.
		/// </summary>
		public static UnfollowIndexResponse UnfollowIndex(this IElasticClient client,IndexName index, Func<UnfollowIndexDescriptor, IUnfollowIndexRequest> selector = null);

		/// <inheritdoc cref="UnfollowIndex(IndexName, System.Func{Nest.UnfollowIndexDescriptor,Nest.IUnfollowIndexRequest})" />
		public static UnfollowIndexResponse UnfollowIndex(this IElasticClient client,IUnfollowIndexRequest request);

		/// <inheritdoc cref="UnfollowIndex(IndexName, System.Func{Nest.UnfollowIndexDescriptor,Nest.IUnfollowIndexRequest})" />
		public static Task<UnfollowIndexResponse> UnfollowIndexAsync(this IElasticClient client,IndexName index, Func<UnfollowIndexDescriptor, IUnfollowIndexRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc cref="UnfollowIndex(IndexName, System.Func{Nest.UnfollowIndexDescriptor,Nest.IUnfollowIndexRequest})" />
		public static Task<UnfollowIndexResponse> UnfollowIndexAsync(this IElasticClient client,IUnfollowIndexRequest request, CancellationToken ct = default);
	}

}
