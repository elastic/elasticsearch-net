using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Pauses a follower index. When this API returns, the follower index will not fetch any additional operations from
		/// the leader index. You can resume following with the resume follower API. Pausing and resuming a follower index can be
		/// used to change the configuration of the following task.
		/// </summary>
		PauseFollowIndexResponse PauseFollowIndex(IndexName index, Func<PauseFollowIndexDescriptor, IPauseFollowIndexRequest> selector = null);

		/// <inheritdoc cref="PauseFollowIndex(IndexName, System.Func{Nest.PauseFollowIndexDescriptor,Nest.IPauseFollowIndexRequest})" />
		PauseFollowIndexResponse PauseFollowIndex(IPauseFollowIndexRequest request);

		/// <inheritdoc cref="PauseFollowIndex(IndexName, System.Func{Nest.PauseFollowIndexDescriptor,Nest.IPauseFollowIndexRequest})" />
		Task<PauseFollowIndexResponse> PauseFollowIndexAsync(IndexName index, Func<PauseFollowIndexDescriptor, IPauseFollowIndexRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc cref="PauseFollowIndex(IndexName, System.Func{Nest.PauseFollowIndexDescriptor,Nest.IPauseFollowIndexRequest})" />
		Task<PauseFollowIndexResponse> PauseFollowIndexAsync(IPauseFollowIndexRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc cref="PauseFollowIndex(IndexName, System.Func{Nest.PauseFollowIndexDescriptor,Nest.IPauseFollowIndexRequest})" />
		public PauseFollowIndexResponse PauseFollowIndex(IndexName index, Func<PauseFollowIndexDescriptor, IPauseFollowIndexRequest> selector = null) =>
			PauseFollowIndex(selector.InvokeOrDefault(new PauseFollowIndexDescriptor(index)));

		/// <inheritdoc cref="PauseFollowIndex(IndexName, System.Func{Nest.PauseFollowIndexDescriptor,Nest.IPauseFollowIndexRequest})" />
		public PauseFollowIndexResponse PauseFollowIndex(IPauseFollowIndexRequest request) =>
			DoRequest<IPauseFollowIndexRequest, PauseFollowIndexResponse>(request, request.RequestParameters);

		/// <inheritdoc cref="PauseFollowIndex(IndexName, System.Func{Nest.PauseFollowIndexDescriptor,Nest.IPauseFollowIndexRequest})" />
		public Task<PauseFollowIndexResponse> PauseFollowIndexAsync(
			IndexName index,
			Func<PauseFollowIndexDescriptor, IPauseFollowIndexRequest> selector = null,
			CancellationToken ct = default
		) => PauseFollowIndexAsync(selector.InvokeOrDefault(new PauseFollowIndexDescriptor(index)), ct);

		/// <inheritdoc cref="PauseFollowIndex(IndexName, System.Func{Nest.PauseFollowIndexDescriptor,Nest.IPauseFollowIndexRequest})" />
		public Task<PauseFollowIndexResponse> PauseFollowIndexAsync(IPauseFollowIndexRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IPauseFollowIndexRequest, PauseFollowIndexResponse>(request, request.RequestParameters, ct);
	}
}
