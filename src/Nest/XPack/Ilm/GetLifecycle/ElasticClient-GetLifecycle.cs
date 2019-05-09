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
		GetLifecycleResponse GetLifecycle(Func<GetLifecycleDescriptor, IGetLifecycleRequest> selector = null);

		/// <inheritdoc cref="GetLifecycle(System.Func{Nest.GetLifecycleDescriptor,Nest.IGetLifecycleRequest})" />
		GetLifecycleResponse GetLifecycle(IGetLifecycleRequest request);

		/// <inheritdoc cref="GetLifecycle(System.Func{Nest.GetLifecycleDescriptor,Nest.IGetLifecycleRequest})" />
		Task<GetLifecycleResponse> GetLifecycleAsync(Func<GetLifecycleDescriptor, IGetLifecycleRequest> selector = null,
			CancellationToken cancellationToken = default
		);

		/// <inheritdoc cref="GetLifecycle(System.Func{Nest.GetLifecycleDescriptor,Nest.IGetLifecycleRequest})" />
		Task<GetLifecycleResponse> GetLifecycleAsync(IGetLifecycleRequest request,
			CancellationToken cancellationToken = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc cref="GetLifecycle(System.Func{Nest.GetLifecycleDescriptor,Nest.IGetLifecycleRequest})" />
		public GetLifecycleResponse GetLifecycle(Func<GetLifecycleDescriptor, IGetLifecycleRequest> selector = null) =>
			GetLifecycle(selector.InvokeOrDefault(new GetLifecycleDescriptor()));

		/// <inheritdoc cref="GetLifecycle(System.Func{Nest.GetLifecycleDescriptor,Nest.IGetLifecycleRequest})" />
		public GetLifecycleResponse GetLifecycle(IGetLifecycleRequest request) =>
			DoRequest<IGetLifecycleRequest, GetLifecycleResponse>(request, request.RequestParameters);

		/// <inheritdoc cref="GetLifecycle(System.Func{Nest.GetLifecycleDescriptor,Nest.IGetLifecycleRequest})" />
		public Task<GetLifecycleResponse> GetLifecycleAsync(Func<GetLifecycleDescriptor, IGetLifecycleRequest> selector = null,
			CancellationToken cancellationToken = default
		) =>
			GetLifecycleAsync(selector.InvokeOrDefault(new GetLifecycleDescriptor()), cancellationToken);

		/// <inheritdoc cref="GetLifecycle(System.Func{Nest.GetLifecycleDescriptor,Nest.IGetLifecycleRequest})" />
		public Task<GetLifecycleResponse> GetLifecycleAsync(IGetLifecycleRequest request,
			CancellationToken cancellationToken = default
		) =>
			DoRequestAsync<IGetLifecycleRequest, GetLifecycleResponse>(request, request.RequestParameters, cancellationToken);
	}
}
