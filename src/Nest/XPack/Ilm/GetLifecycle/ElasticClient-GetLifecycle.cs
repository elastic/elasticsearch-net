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
		IGetLifecycleResponse GetLifecycle(Func<GetLifecycleDescriptor, IGetLifecycleRequest> selector = null);

		/// <inheritdoc cref="GetLifecycle(System.Func{Nest.GetLifecycleDescriptor,Nest.IGetLifecycleRequest})" />
		IGetLifecycleResponse GetLifecycle(IGetLifecycleRequest request);

		/// <inheritdoc cref="GetLifecycle(System.Func{Nest.GetLifecycleDescriptor,Nest.IGetLifecycleRequest})" />
		Task<IGetLifecycleResponse> GetLifecycleAsync(Func<GetLifecycleDescriptor, IGetLifecycleRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc cref="GetLifecycle(System.Func{Nest.GetLifecycleDescriptor,Nest.IGetLifecycleRequest})" />
		Task<IGetLifecycleResponse> GetLifecycleAsync(IGetLifecycleRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc cref="GetLifecycle(System.Func{Nest.GetLifecycleDescriptor,Nest.IGetLifecycleRequest})" />
		public IGetLifecycleResponse GetLifecycle(Func<GetLifecycleDescriptor, IGetLifecycleRequest> selector = null) =>
			GetLifecycle(selector.InvokeOrDefault(new GetLifecycleDescriptor()));

		/// <inheritdoc cref="GetLifecycle(System.Func{Nest.GetLifecycleDescriptor,Nest.IGetLifecycleRequest})" />
		public IGetLifecycleResponse GetLifecycle(IGetLifecycleRequest request) =>
			Dispatcher.Dispatch<IGetLifecycleRequest, GetLifecycleRequestParameters, GetLifecycleResponse>(
				request,
				(p, d) => LowLevelDispatch.XpackIlmGetLifecycleDispatch<GetLifecycleResponse>(p)
			);

		/// <inheritdoc cref="GetLifecycle(System.Func{Nest.GetLifecycleDescriptor,Nest.IGetLifecycleRequest})" />
		public Task<IGetLifecycleResponse> GetLifecycleAsync(Func<GetLifecycleDescriptor, IGetLifecycleRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			GetLifecycleAsync(selector.InvokeOrDefault(new GetLifecycleDescriptor()), cancellationToken);

		/// <inheritdoc cref="GetLifecycle(System.Func{Nest.GetLifecycleDescriptor,Nest.IGetLifecycleRequest})" />
		public Task<IGetLifecycleResponse> GetLifecycleAsync(IGetLifecycleRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			Dispatcher.DispatchAsync<IGetLifecycleRequest, GetLifecycleRequestParameters, GetLifecycleResponse, IGetLifecycleResponse>(
				request,
				cancellationToken,
				(p, d, c) => LowLevelDispatch.XpackIlmGetLifecycleDispatchAsync<GetLifecycleResponse>(p, c)
			);
	}
}
