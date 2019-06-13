using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <summary>
		/// Creates a new follower index that is configured to follow the referenced leader index.
		/// When this API returns, the follower index exists, and cross-cluster replication starts replicating
		/// operations from the leader index to the follower index.
		/// </summary>
		public static CreateFollowIndexResponse CreateFollowIndex(this IElasticClient client,IndexName index, Func<CreateFollowIndexDescriptor, ICreateFollowIndexRequest> selector);

		/// <inheritdoc cref="CreateFollowIndex(IndexName, System.Func{Nest.CreateFollowIndexDescriptor,Nest.ICreateFollowIndexRequest})" />
		public static CreateFollowIndexResponse CreateFollowIndex(this IElasticClient client,ICreateFollowIndexRequest request);

		/// <inheritdoc cref="CreateFollowIndex(IndexName, System.Func{Nest.CreateFollowIndexDescriptor,Nest.ICreateFollowIndexRequest})" />
		public static Task<CreateFollowIndexResponse> CreateFollowIndexAsync(this IElasticClient client,IndexName index, Func<CreateFollowIndexDescriptor, ICreateFollowIndexRequest> selector,
			CancellationToken ct = default
		);

		/// <inheritdoc cref="CreateFollowIndex(IndexName, System.Func{Nest.CreateFollowIndexDescriptor,Nest.ICreateFollowIndexRequest})" />
		public static Task<CreateFollowIndexResponse> CreateFollowIndexAsync(this IElasticClient client,ICreateFollowIndexRequest request, CancellationToken ct = default);
	}

}
