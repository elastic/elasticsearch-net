using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Creates or updates lifecycle policy.
		/// Creates a lifecycle policy. If the specified policy exists, the policy is replaced and the policy version is incremented.
		/// </summary>
		IPutLifecycleResponse PutLifecycle(PolicyId policyId, Func<PutLifecycleDescriptor, IPutLifecycleRequest> selector = null);

		/// <inheritdoc cref="PutLifecycle(Nest.PolicyId,System.Func{Nest.PutLifecycleDescriptor,Nest.IPutLifecycleRequest})" />
		IPutLifecycleResponse PutLifecycle(IPutLifecycleRequest request);

		/// <inheritdoc cref="PutLifecycle(Nest.PolicyId,System.Func{Nest.PutLifecycleDescriptor,Nest.IPutLifecycleRequest})" />
		Task<IPutLifecycleResponse> PutLifecycleAsync(PolicyId policyId, Func<PutLifecycleDescriptor, IPutLifecycleRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc cref="PutLifecycle(Nest.PolicyId,System.Func{Nest.PutLifecycleDescriptor,Nest.IPutLifecycleRequest})" />
		Task<IPutLifecycleResponse> PutLifecycleAsync(IPutLifecycleRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc cref="PutLifecycle(Nest.PolicyId,System.Func{Nest.PutLifecycleDescriptor,Nest.IPutLifecycleRequest})" />
		public IPutLifecycleResponse PutLifecycle(PolicyId policyId, Func<PutLifecycleDescriptor, IPutLifecycleRequest> selector = null) =>
			PutLifecycle(selector.InvokeOrDefault(new PutLifecycleDescriptor(policyId)));

		/// <inheritdoc cref="PutLifecycle(Nest.PolicyId,System.Func{Nest.PutLifecycleDescriptor,Nest.IPutLifecycleRequest})" />
		public IPutLifecycleResponse PutLifecycle(IPutLifecycleRequest request) =>
			Dispatcher.Dispatch<IPutLifecycleRequest, PutLifecycleRequestParameters, PutLifecycleResponse>(
				request,
				LowLevelDispatch.XpackIlmPutLifecycleDispatch<PutLifecycleResponse>
			);

		/// <inheritdoc cref="PutLifecycle(Nest.PolicyId,System.Func{Nest.PutLifecycleDescriptor,Nest.IPutLifecycleRequest})" />
		public Task<IPutLifecycleResponse> PutLifecycleAsync(PolicyId policyId, Func<PutLifecycleDescriptor, IPutLifecycleRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			PutLifecycleAsync(selector.InvokeOrDefault(new PutLifecycleDescriptor(policyId)), cancellationToken);

		/// <inheritdoc cref="PutLifecycle(Nest.PolicyId,System.Func{Nest.PutLifecycleDescriptor,Nest.IPutLifecycleRequest})" />
		public Task<IPutLifecycleResponse> PutLifecycleAsync(IPutLifecycleRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			Dispatcher.DispatchAsync<IPutLifecycleRequest, PutLifecycleRequestParameters, PutLifecycleResponse, IPutLifecycleResponse>(
				request,
				cancellationToken,
				LowLevelDispatch.XpackIlmPutLifecycleDispatchAsync<PutLifecycleResponse>
			);
	}
}
