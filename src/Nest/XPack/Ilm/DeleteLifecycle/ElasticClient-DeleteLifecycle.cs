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
		DeleteLifecycleResponse DeleteLifecycle(PolicyId policyId, Func<DeleteLifecycleDescriptor, IDeleteLifecycleRequest> selector = null);

		/// <inheritdoc cref="DeleteLifecycle(Nest.PolicyId,System.Func{Nest.DeleteLifecycleDescriptor,Nest.IDeleteLifecycleRequest})" />
		DeleteLifecycleResponse DeleteLifecycle(IDeleteLifecycleRequest request);

		/// <inheritdoc cref="DeleteLifecycle(Nest.PolicyId,System.Func{Nest.DeleteLifecycleDescriptor,Nest.IDeleteLifecycleRequest})" />
		Task<DeleteLifecycleResponse> DeleteLifecycleAsync(PolicyId policyId, Func<DeleteLifecycleDescriptor, IDeleteLifecycleRequest> selector = null,
			CancellationToken cancellationToken = default
		);

		/// <inheritdoc cref="DeleteLifecycle(Nest.PolicyId,System.Func{Nest.DeleteLifecycleDescriptor,Nest.IDeleteLifecycleRequest})" />
		Task<DeleteLifecycleResponse> DeleteLifecycleAsync(IDeleteLifecycleRequest request,
			CancellationToken cancellationToken = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc cref="DeleteLifecycle(Nest.PolicyId,System.Func{Nest.DeleteLifecycleDescriptor,Nest.IDeleteLifecycleRequest})" />
		public DeleteLifecycleResponse DeleteLifecycle(PolicyId policyId, Func<DeleteLifecycleDescriptor, IDeleteLifecycleRequest> selector = null) =>
			DeleteLifecycle(selector.InvokeOrDefault(new DeleteLifecycleDescriptor(policyId)));

		/// <inheritdoc cref="DeleteLifecycle(Nest.PolicyId,System.Func{Nest.DeleteLifecycleDescriptor,Nest.IDeleteLifecycleRequest})" />
		public DeleteLifecycleResponse DeleteLifecycle(IDeleteLifecycleRequest request) =>
			DoRequest<IDeleteLifecycleRequest, DeleteLifecycleResponse>(request, request.RequestParameters);

		/// <inheritdoc cref="DeleteLifecycle(Nest.PolicyId,System.Func{Nest.DeleteLifecycleDescriptor,Nest.IDeleteLifecycleRequest})" />
		public Task<DeleteLifecycleResponse> DeleteLifecycleAsync(PolicyId policyId, Func<DeleteLifecycleDescriptor, IDeleteLifecycleRequest> selector = null,
			CancellationToken cancellationToken = default
		) =>
			DeleteLifecycleAsync(selector.InvokeOrDefault(new DeleteLifecycleDescriptor(policyId)), cancellationToken);

		/// <inheritdoc cref="DeleteLifecycle(Nest.PolicyId,System.Func{Nest.DeleteLifecycleDescriptor,Nest.IDeleteLifecycleRequest})" />
		public Task<DeleteLifecycleResponse> DeleteLifecycleAsync(IDeleteLifecycleRequest request,
			CancellationToken cancellationToken = default
		) =>
			DoRequestAsync<IDeleteLifecycleRequest, DeleteLifecycleResponse>(request, request.RequestParameters, cancellationToken);
	}
}
