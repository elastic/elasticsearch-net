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
		IIlmPutLifecycleResponse IlmPutLifecycle(Policy policy, Func<IlmPutLifecycleDescriptor, IIlmPutLifecycleRequest> selector = null);

		/// <inheritdoc cref="IlmPutLifecycle(Nest.Policy,System.Func{Nest.IlmPutLifecycleDescriptor,Nest.IIlmPutLifecycleRequest})" />
		IIlmPutLifecycleResponse IlmPutLifecycle(IIlmPutLifecycleRequest request);

		/// <inheritdoc cref="IlmPutLifecycle(Nest.Policy,System.Func{Nest.IlmPutLifecycleDescriptor,Nest.IIlmPutLifecycleRequest})" />
		Task<IIlmPutLifecycleResponse> IlmPutLifecycleAsync(Policy policy, Func<IlmPutLifecycleDescriptor, IIlmPutLifecycleRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc cref="IlmPutLifecycle(Nest.Policy,System.Func{Nest.IlmPutLifecycleDescriptor,Nest.IIlmPutLifecycleRequest})" />
		Task<IIlmPutLifecycleResponse> IlmPutLifecycleAsync(IIlmPutLifecycleRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc cref="IlmPutLifecycle(Nest.Policy,System.Func{Nest.IlmPutLifecycleDescriptor,Nest.IIlmPutLifecycleRequest})" />
		public IIlmPutLifecycleResponse IlmPutLifecycle(Policy policy, Func<IlmPutLifecycleDescriptor, IIlmPutLifecycleRequest> selector = null) =>
			IlmPutLifecycle(selector.InvokeOrDefault(new IlmPutLifecycleDescriptor(policy)));

		/// <inheritdoc cref="IlmPutLifecycle(Nest.Policy,System.Func{Nest.IlmPutLifecycleDescriptor,Nest.IIlmPutLifecycleRequest})" />
		public IIlmPutLifecycleResponse IlmPutLifecycle(IIlmPutLifecycleRequest request) =>
			Dispatcher.Dispatch<IIlmPutLifecycleRequest, IlmPutLifecycleRequestParameters, IlmPutLifecycleResponse>(
				request,
				LowLevelDispatch.IlmPutLifecycleDispatch<IlmPutLifecycleResponse>
			);

		/// <inheritdoc cref="IlmPutLifecycle(Nest.Policy,System.Func{Nest.IlmPutLifecycleDescriptor,Nest.IIlmPutLifecycleRequest})" />
		public Task<IIlmPutLifecycleResponse> IlmPutLifecycleAsync(Policy policy, Func<IlmPutLifecycleDescriptor, IIlmPutLifecycleRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			IlmPutLifecycleAsync(selector.InvokeOrDefault(new IlmPutLifecycleDescriptor(policy)), cancellationToken);

		/// <inheritdoc cref="IlmPutLifecycle(Nest.Policy,System.Func{Nest.IlmPutLifecycleDescriptor,Nest.IIlmPutLifecycleRequest})" />
		public Task<IIlmPutLifecycleResponse> IlmPutLifecycleAsync(IIlmPutLifecycleRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			Dispatcher.DispatchAsync<IIlmPutLifecycleRequest, IlmPutLifecycleRequestParameters, IlmPutLifecycleResponse, IIlmPutLifecycleResponse>(
				request,
				cancellationToken,
				LowLevelDispatch.IlmPutLifecycleDispatchAsync<IlmPutLifecycleResponse>
			);
	}
}
