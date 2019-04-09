using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Retrieves a lifecycle policy.
		/// Returns the specified policy definition. Includes the policy version and last modified date.
		/// If no policy is specified, returns all defined policies.
		/// </summary>
		IIlmGetLifecycleResponse IlmGetLifecycle(Func<IlmGetLifecycleDescriptor, IIlmGetLifecycleRequest> selector = null);

		/// <inheritdoc cref="IlmGetLifecycle(System.Func{Nest.IlmGetLifecycleDescriptor,Nest.IIlmGetLifecycleRequest})" />
		IIlmGetLifecycleResponse IlmGetLifecycle(IIlmGetLifecycleRequest request);

		/// <inheritdoc cref="IlmGetLifecycle(System.Func{Nest.IlmGetLifecycleDescriptor,Nest.IIlmGetLifecycleRequest})" />
		Task<IIlmGetLifecycleResponse> IlmGetLifecycleAsync(Func<IlmGetLifecycleDescriptor, IIlmGetLifecycleRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc cref="IlmGetLifecycle(System.Func{Nest.IlmGetLifecycleDescriptor,Nest.IIlmGetLifecycleRequest})" />
		Task<IIlmGetLifecycleResponse> IlmGetLifecycleAsync(IIlmGetLifecycleRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc cref="IlmGetLifecycle(System.Func{Nest.IlmGetLifecycleDescriptor,Nest.IIlmGetLifecycleRequest})" />
		public IIlmGetLifecycleResponse IlmGetLifecycle(Func<IlmGetLifecycleDescriptor, IIlmGetLifecycleRequest> selector = null) =>
			IlmGetLifecycle(selector.InvokeOrDefault(new IlmGetLifecycleDescriptor()));

		/// <inheritdoc cref="IlmGetLifecycle(System.Func{Nest.IlmGetLifecycleDescriptor,Nest.IIlmGetLifecycleRequest})" />
		public IIlmGetLifecycleResponse IlmGetLifecycle(IIlmGetLifecycleRequest request) =>
			Dispatcher.Dispatch<IIlmGetLifecycleRequest, IlmGetLifecycleRequestParameters, IlmGetLifecycleResponse>(
				request,
				(p, d) => LowLevelDispatch.IlmGetLifecycleDispatch<IlmGetLifecycleResponse>(p)
			);

		/// <inheritdoc cref="IlmGetLifecycle(System.Func{Nest.IlmGetLifecycleDescriptor,Nest.IIlmGetLifecycleRequest})" />
		public Task<IIlmGetLifecycleResponse> IlmGetLifecycleAsync(Func<IlmGetLifecycleDescriptor, IIlmGetLifecycleRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			IlmGetLifecycleAsync(selector.InvokeOrDefault(new IlmGetLifecycleDescriptor()), cancellationToken);

		/// <inheritdoc cref="IlmGetLifecycle(System.Func{Nest.IlmGetLifecycleDescriptor,Nest.IIlmGetLifecycleRequest})" />
		public Task<IIlmGetLifecycleResponse> IlmGetLifecycleAsync(IIlmGetLifecycleRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			Dispatcher.DispatchAsync<IIlmGetLifecycleRequest, IlmGetLifecycleRequestParameters, IlmGetLifecycleResponse, IIlmGetLifecycleResponse>(
				request,
				cancellationToken,
				(p, d, c) => LowLevelDispatch.IlmGetLifecycleDispatchAsync<IlmGetLifecycleResponse>(p, c)
			);
	}
}
