using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Stops the following task associated with a follower index and removes index metadata and settings associated with
		/// cross-cluster replication. This enables the index to treated as a regular index. The follower index must be paused and closed
		/// before invoking the unfollow API.
		/// </summary>
		UnfollowIndexResponse UnfollowIndex(IndexName index, Func<UnfollowIndexDescriptor, IUnfollowIndexRequest> selector = null);

		/// <inheritdoc cref="UnfollowIndex(IndexName, System.Func{Nest.UnfollowIndexDescriptor,Nest.IUnfollowIndexRequest})" />
		UnfollowIndexResponse UnfollowIndex(IUnfollowIndexRequest request);

		/// <inheritdoc cref="UnfollowIndex(IndexName, System.Func{Nest.UnfollowIndexDescriptor,Nest.IUnfollowIndexRequest})" />
		Task<UnfollowIndexResponse> UnfollowIndexAsync(IndexName index, Func<UnfollowIndexDescriptor, IUnfollowIndexRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc cref="UnfollowIndex(IndexName, System.Func{Nest.UnfollowIndexDescriptor,Nest.IUnfollowIndexRequest})" />
		Task<UnfollowIndexResponse> UnfollowIndexAsync(IUnfollowIndexRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc cref="UnfollowIndex(IndexName, System.Func{Nest.UnfollowIndexDescriptor,Nest.IUnfollowIndexRequest})" />
		public UnfollowIndexResponse UnfollowIndex(IndexName index, Func<UnfollowIndexDescriptor, IUnfollowIndexRequest> selector = null) =>
			UnfollowIndex(selector.InvokeOrDefault(new UnfollowIndexDescriptor(index)));

		/// <inheritdoc cref="UnfollowIndex(IndexName, System.Func{Nest.UnfollowIndexDescriptor,Nest.IUnfollowIndexRequest})" />
		public UnfollowIndexResponse UnfollowIndex(IUnfollowIndexRequest request) =>
			DoRequest<IUnfollowIndexRequest, UnfollowIndexResponse>(request, request.RequestParameters);

		/// <inheritdoc cref="UnfollowIndex(IndexName, System.Func{Nest.UnfollowIndexDescriptor,Nest.IUnfollowIndexRequest})" />
		public Task<UnfollowIndexResponse> UnfollowIndexAsync(
			IndexName index,
			Func<UnfollowIndexDescriptor, IUnfollowIndexRequest> selector = null,
			CancellationToken ct = default
		) => UnfollowIndexAsync(selector.InvokeOrDefault(new UnfollowIndexDescriptor(index)), ct);

		/// <inheritdoc cref="UnfollowIndex(IndexName, System.Func{Nest.UnfollowIndexDescriptor,Nest.IUnfollowIndexRequest})" />
		public Task<UnfollowIndexResponse> UnfollowIndexAsync(IUnfollowIndexRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IUnfollowIndexRequest, UnfollowIndexResponse>(request, request.RequestParameters, ct);
	}
}
