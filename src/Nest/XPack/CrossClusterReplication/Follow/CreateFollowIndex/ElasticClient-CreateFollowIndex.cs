using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Creates a new follower index that is configured to follow the referenced leader index.
		/// When this API returns, the follower index exists, and cross-cluster replication starts replicating
		/// operations from the leader index to the follower index.
		/// </summary>
		CreateFollowIndexResponse CreateFollowIndex(IndexName index, Func<CreateFollowIndexDescriptor, ICreateFollowIndexRequest> selector);

		/// <inheritdoc cref="CreateFollowIndex(IndexName, System.Func{Nest.CreateFollowIndexDescriptor,Nest.ICreateFollowIndexRequest})" />
		CreateFollowIndexResponse CreateFollowIndex(ICreateFollowIndexRequest request);

		/// <inheritdoc cref="CreateFollowIndex(IndexName, System.Func{Nest.CreateFollowIndexDescriptor,Nest.ICreateFollowIndexRequest})" />
		Task<CreateFollowIndexResponse> CreateFollowIndexAsync(IndexName index, Func<CreateFollowIndexDescriptor, ICreateFollowIndexRequest> selector,
			CancellationToken ct = default
		);

		/// <inheritdoc cref="CreateFollowIndex(IndexName, System.Func{Nest.CreateFollowIndexDescriptor,Nest.ICreateFollowIndexRequest})" />
		Task<CreateFollowIndexResponse> CreateFollowIndexAsync(ICreateFollowIndexRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc cref="CreateFollowIndex(IndexName, System.Func{Nest.CreateFollowIndexDescriptor,Nest.ICreateFollowIndexRequest})" />
		public CreateFollowIndexResponse CreateFollowIndex(IndexName index, Func<CreateFollowIndexDescriptor, ICreateFollowIndexRequest> selector) =>
			CreateFollowIndex(selector.InvokeOrDefault(new CreateFollowIndexDescriptor(index)));

		/// <inheritdoc cref="CreateFollowIndex(IndexName, System.Func{Nest.CreateFollowIndexDescriptor,Nest.ICreateFollowIndexRequest})" />
		public CreateFollowIndexResponse CreateFollowIndex(ICreateFollowIndexRequest request) =>
			DoRequest<ICreateFollowIndexRequest, CreateFollowIndexResponse>(request, request.RequestParameters);

		/// <inheritdoc cref="CreateFollowIndex(IndexName, System.Func{Nest.CreateFollowIndexDescriptor,Nest.ICreateFollowIndexRequest})" />
		public Task<CreateFollowIndexResponse> CreateFollowIndexAsync(
			IndexName index,
			Func<CreateFollowIndexDescriptor, ICreateFollowIndexRequest> selector,
			CancellationToken ct = default
		) =>
			CreateFollowIndexAsync(selector.InvokeOrDefault(new CreateFollowIndexDescriptor(index)), ct);

		/// <inheritdoc cref="CreateFollowIndex(IndexName, System.Func{Nest.CreateFollowIndexDescriptor,Nest.ICreateFollowIndexRequest})" />
		public Task<CreateFollowIndexResponse> CreateFollowIndexAsync(ICreateFollowIndexRequest request, CancellationToken ct = default) =>
			DoRequestAsync<ICreateFollowIndexRequest, CreateFollowIndexResponse, CreateFollowIndexResponse>(request, request.RequestParameters, ct);
	}
}
