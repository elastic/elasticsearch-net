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
		IDeleteLifecycleResponse DeleteLifecycle(PolicyId policyId, Func<DeleteLifecycleDescriptor, IDeleteLifecycleRequest> selector = null);

		/// <inheritdoc cref="DeleteLifecycle(Nest.PolicyId,System.Func{Nest.DeleteLifecycleDescriptor,Nest.IDeleteLifecycleRequest})" />
		IDeleteLifecycleResponse DeleteLifecycle(IDeleteLifecycleRequest request);

		/// <inheritdoc cref="DeleteLifecycle(Nest.PolicyId,System.Func{Nest.DeleteLifecycleDescriptor,Nest.IDeleteLifecycleRequest})" />
		Task<IDeleteLifecycleResponse> DeleteLifecycleAsync(PolicyId policyId, Func<DeleteLifecycleDescriptor, IDeleteLifecycleRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc cref="DeleteLifecycle(Nest.PolicyId,System.Func{Nest.DeleteLifecycleDescriptor,Nest.IDeleteLifecycleRequest})" />
		Task<IDeleteLifecycleResponse> DeleteLifecycleAsync(IDeleteLifecycleRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc cref="DeleteLifecycle(Nest.PolicyId,System.Func{Nest.DeleteLifecycleDescriptor,Nest.IDeleteLifecycleRequest})" />
		public IDeleteLifecycleResponse DeleteLifecycle(PolicyId policyId, Func<DeleteLifecycleDescriptor, IDeleteLifecycleRequest> selector = null) =>
			DeleteLifecycle(selector.InvokeOrDefault(new DeleteLifecycleDescriptor(policyId)));

		/// <inheritdoc cref="DeleteLifecycle(Nest.PolicyId,System.Func{Nest.DeleteLifecycleDescriptor,Nest.IDeleteLifecycleRequest})" />
		public IDeleteLifecycleResponse DeleteLifecycle(IDeleteLifecycleRequest request) =>
			Dispatcher.Dispatch<IDeleteLifecycleRequest, DeleteLifecycleRequestParameters, DeleteLifecycleResponse>(
				request,
				(p, d) => LowLevelDispatch.XpackIlmDeleteLifecycleDispatch<DeleteLifecycleResponse>(p)
			);

		/// <inheritdoc cref="DeleteLifecycle(Nest.PolicyId,System.Func{Nest.DeleteLifecycleDescriptor,Nest.IDeleteLifecycleRequest})" />
		public Task<IDeleteLifecycleResponse> DeleteLifecycleAsync(PolicyId policyId, Func<DeleteLifecycleDescriptor, IDeleteLifecycleRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			DeleteLifecycleAsync(selector.InvokeOrDefault(new DeleteLifecycleDescriptor(policyId)), cancellationToken);

		/// <inheritdoc cref="DeleteLifecycle(Nest.PolicyId,System.Func{Nest.DeleteLifecycleDescriptor,Nest.IDeleteLifecycleRequest})" />
		public Task<IDeleteLifecycleResponse> DeleteLifecycleAsync(IDeleteLifecycleRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			Dispatcher.DispatchAsync<IDeleteLifecycleRequest, DeleteLifecycleRequestParameters, DeleteLifecycleResponse, IDeleteLifecycleResponse>(
				request,
				cancellationToken,
				(p, d, c) => LowLevelDispatch.XpackIlmDeleteLifecycleDispatchAsync<DeleteLifecycleResponse>(p, c)
			);
	}
}
