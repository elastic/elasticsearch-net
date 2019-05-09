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
		PutLifecycleResponse PutLifecycle(PolicyId policyId, Func<PutLifecycleDescriptor, IPutLifecycleRequest> selector = null);

		/// <inheritdoc cref="PutLifecycle(Nest.PolicyId,System.Func{Nest.PutLifecycleDescriptor,Nest.IPutLifecycleRequest})" />
		PutLifecycleResponse PutLifecycle(IPutLifecycleRequest request);

		/// <inheritdoc cref="PutLifecycle(Nest.PolicyId,System.Func{Nest.PutLifecycleDescriptor,Nest.IPutLifecycleRequest})" />
		Task<PutLifecycleResponse> PutLifecycleAsync(PolicyId policyId, Func<PutLifecycleDescriptor, IPutLifecycleRequest> selector = null,
			CancellationToken cancellationToken = default
		);

		/// <inheritdoc cref="PutLifecycle(Nest.PolicyId,System.Func{Nest.PutLifecycleDescriptor,Nest.IPutLifecycleRequest})" />
		Task<PutLifecycleResponse> PutLifecycleAsync(IPutLifecycleRequest request,
			CancellationToken cancellationToken = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc cref="PutLifecycle(Nest.PolicyId,System.Func{Nest.PutLifecycleDescriptor,Nest.IPutLifecycleRequest})" />
		public PutLifecycleResponse PutLifecycle(PolicyId policyId, Func<PutLifecycleDescriptor, IPutLifecycleRequest> selector = null) =>
			PutLifecycle(selector.InvokeOrDefault(new PutLifecycleDescriptor(policyId)));

		/// <inheritdoc cref="PutLifecycle(Nest.PolicyId,System.Func{Nest.PutLifecycleDescriptor,Nest.IPutLifecycleRequest})" />
		public PutLifecycleResponse PutLifecycle(IPutLifecycleRequest request) =>
			DoRequest<IPutLifecycleRequest, PutLifecycleResponse>(request, request.RequestParameters);

		/// <inheritdoc cref="PutLifecycle(Nest.PolicyId,System.Func{Nest.PutLifecycleDescriptor,Nest.IPutLifecycleRequest})" />
		public Task<PutLifecycleResponse> PutLifecycleAsync(PolicyId policyId, Func<PutLifecycleDescriptor, IPutLifecycleRequest> selector = null,
			CancellationToken cancellationToken = default
		) =>
			PutLifecycleAsync(selector.InvokeOrDefault(new PutLifecycleDescriptor(policyId)), cancellationToken);

		/// <inheritdoc cref="PutLifecycle(Nest.PolicyId,System.Func{Nest.PutLifecycleDescriptor,Nest.IPutLifecycleRequest})" />
		public Task<PutLifecycleResponse> PutLifecycleAsync(IPutLifecycleRequest request,
			CancellationToken cancellationToken = default
		) =>
			DoRequestAsync<IPutLifecycleRequest, PutLifecycleResponse>(request, request.RequestParameters, cancellationToken);
	}
}
