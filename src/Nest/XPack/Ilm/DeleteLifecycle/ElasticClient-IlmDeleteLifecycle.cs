using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Deletes a lifecycle policy.
		/// Deletes the specified lifecycle policy definition. You cannot delete policies that are currently in use.
		/// If the policy is being used to manage any indices, the request fails and returns an error.
		/// </summary>
		IIlmDeleteLifecycleResponse IlmDeleteLifecycle(PolicyId policyId, Func<IlmDeleteLifecycleDescriptor, IIlmDeleteLifecycleRequest> selector = null);

		/// <inheritdoc cref="IlmDeleteLifecycle(Nest.PolicyId,System.Func{Nest.IlmDeleteLifecycleDescriptor,Nest.IIlmDeleteLifecycleRequest})" />
		IIlmDeleteLifecycleResponse IlmDeleteLifecycle(IIlmDeleteLifecycleRequest request);

		/// <inheritdoc cref="IlmDeleteLifecycle(Nest.PolicyId,System.Func{Nest.IlmDeleteLifecycleDescriptor,Nest.IIlmDeleteLifecycleRequest})" />
		Task<IIlmDeleteLifecycleResponse> IlmDeleteLifecycleAsync(PolicyId policyId, Func<IlmDeleteLifecycleDescriptor, IIlmDeleteLifecycleRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc cref="IlmDeleteLifecycle(Nest.PolicyId,System.Func{Nest.IlmDeleteLifecycleDescriptor,Nest.IIlmDeleteLifecycleRequest})" />
		Task<IIlmDeleteLifecycleResponse> IlmDeleteLifecycleAsync(IIlmDeleteLifecycleRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc cref="IlmDeleteLifecycle(Nest.PolicyId,System.Func{Nest.IlmDeleteLifecycleDescriptor,Nest.IIlmDeleteLifecycleRequest})" />
		public IIlmDeleteLifecycleResponse IlmDeleteLifecycle(PolicyId policyId, Func<IlmDeleteLifecycleDescriptor, IIlmDeleteLifecycleRequest> selector = null) =>
			IlmDeleteLifecycle(selector.InvokeOrDefault(new IlmDeleteLifecycleDescriptor(policyId)));

		/// <inheritdoc cref="IlmDeleteLifecycle(Nest.PolicyId,System.Func{Nest.IlmDeleteLifecycleDescriptor,Nest.IIlmDeleteLifecycleRequest})" />
		public IIlmDeleteLifecycleResponse IlmDeleteLifecycle(IIlmDeleteLifecycleRequest request) =>
			Dispatcher.Dispatch<IIlmDeleteLifecycleRequest, IlmDeleteLifecycleRequestParameters, IlmDeleteLifecycleResponse>(
				request,
				(p, d) => LowLevelDispatch.IlmDeleteLifecycleDispatch<IlmDeleteLifecycleResponse>(p)
			);

		/// <inheritdoc cref="IlmDeleteLifecycle(Nest.PolicyId,System.Func{Nest.IlmDeleteLifecycleDescriptor,Nest.IIlmDeleteLifecycleRequest})" />
		public Task<IIlmDeleteLifecycleResponse> IlmDeleteLifecycleAsync(PolicyId policyId, Func<IlmDeleteLifecycleDescriptor, IIlmDeleteLifecycleRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			IlmDeleteLifecycleAsync(selector.InvokeOrDefault(new IlmDeleteLifecycleDescriptor(policyId)), cancellationToken);

		/// <inheritdoc cref="IlmDeleteLifecycle(Nest.PolicyId,System.Func{Nest.IlmDeleteLifecycleDescriptor,Nest.IIlmDeleteLifecycleRequest})" />
		public Task<IIlmDeleteLifecycleResponse> IlmDeleteLifecycleAsync(IIlmDeleteLifecycleRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			Dispatcher.DispatchAsync<IIlmDeleteLifecycleRequest, IlmDeleteLifecycleRequestParameters, IlmDeleteLifecycleResponse, IIlmDeleteLifecycleResponse>(
				request,
				cancellationToken,
				(p, d, c) => LowLevelDispatch.IlmDeleteLifecycleDispatchAsync<IlmDeleteLifecycleResponse>(p, c)
			);
	}
}
