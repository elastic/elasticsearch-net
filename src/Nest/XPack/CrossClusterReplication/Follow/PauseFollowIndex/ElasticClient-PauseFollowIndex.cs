using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// This API pauses a follower index. When this API returns, the follower index will not fetch any additional operations from
		/// the leader index. You can resume following with the resume follower API. Pausing and resuming a follower index can be
		/// used to change the configuration of the following task.
		/// </summary>
		IPauseFollowIndexResponse PauseFollowIndex(IndexName index, Func<PauseFollowIndexDescriptor, IPauseFollowIndexRequest> selector = null);

		/// <inheritdoc cref="PauseFollowIndex(IndexName, System.Func{Nest.PauseFollowIndexDescriptor,Nest.IPauseFollowIndexRequest})" />
		IPauseFollowIndexResponse PauseFollowIndex(IPauseFollowIndexRequest request);

		/// <inheritdoc cref="PauseFollowIndex(IndexName, System.Func{Nest.PauseFollowIndexDescriptor,Nest.IPauseFollowIndexRequest})" />
		Task<IPauseFollowIndexResponse> PauseFollowIndexAsync(IndexName index, Func<PauseFollowIndexDescriptor, IPauseFollowIndexRequest> selector = null,
			CancellationToken cancellationToken = default
		);

		/// <inheritdoc cref="PauseFollowIndex(IndexName, System.Func{Nest.PauseFollowIndexDescriptor,Nest.IPauseFollowIndexRequest})" />
		Task<IPauseFollowIndexResponse> PauseFollowIndexAsync(IPauseFollowIndexRequest request, CancellationToken cancellationToken = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc cref="PauseFollowIndex(IndexName, System.Func{Nest.PauseFollowIndexDescriptor,Nest.IPauseFollowIndexRequest})" />
		public IPauseFollowIndexResponse PauseFollowIndex(IndexName index, Func<PauseFollowIndexDescriptor, IPauseFollowIndexRequest> selector = null) =>
			PauseFollowIndex(selector.InvokeOrDefault(new PauseFollowIndexDescriptor(index)));

		/// <inheritdoc cref="PauseFollowIndex(IndexName, System.Func{Nest.PauseFollowIndexDescriptor,Nest.IPauseFollowIndexRequest})" />
		public IPauseFollowIndexResponse PauseFollowIndex(IPauseFollowIndexRequest request) =>
			Dispatcher.Dispatch<IPauseFollowIndexRequest, PauseFollowIndexRequestParameters, PauseFollowIndexResponse>(
				request,
				(p, d) => LowLevelDispatch.CcrPauseFollowDispatch<PauseFollowIndexResponse>(p)
			);

		/// <inheritdoc cref="PauseFollowIndex(IndexName, System.Func{Nest.PauseFollowIndexDescriptor,Nest.IPauseFollowIndexRequest})" />
		public Task<IPauseFollowIndexResponse> PauseFollowIndexAsync(
			IndexName index,
			Func<PauseFollowIndexDescriptor, IPauseFollowIndexRequest> selector = null,
			CancellationToken cancellationToken = default
		) =>
			PauseFollowIndexAsync(selector.InvokeOrDefault(new PauseFollowIndexDescriptor(index)), cancellationToken);

		/// <inheritdoc cref="PauseFollowIndex(IndexName, System.Func{Nest.PauseFollowIndexDescriptor,Nest.IPauseFollowIndexRequest})" />
		public Task<IPauseFollowIndexResponse> PauseFollowIndexAsync(IPauseFollowIndexRequest request, CancellationToken cancellationToken = default) =>
			Dispatcher.DispatchAsync<IPauseFollowIndexRequest, PauseFollowIndexRequestParameters, PauseFollowIndexResponse, IPauseFollowIndexResponse>(
				request,
				cancellationToken,
				(p, d, c) => LowLevelDispatch.CcrPauseFollowDispatchAsync<PauseFollowIndexResponse>(p, c)
			);
	}
}
