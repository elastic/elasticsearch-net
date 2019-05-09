using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Invalidates one or more API keys.
		/// </summary>
		InvalidateApiKeyResponse InvalidateApiKey(Func<InvalidateApiKeyDescriptor, IInvalidateApiKeyRequest> selector = null);

		/// <inheritdoc cref="InvalidateApiKey(System.Func{InvalidateApiKeyDescriptor,IInvalidateApiKeyRequest})" />
		InvalidateApiKeyResponse InvalidateApiKey(IInvalidateApiKeyRequest request);

		/// <inheritdoc cref="InvalidateApiKey(System.Func{InvalidateApiKeyDescriptor,IInvalidateApiKeyRequest})" />
		Task<InvalidateApiKeyResponse> InvalidateApiKeyAsync(
			Func<InvalidateApiKeyDescriptor, IInvalidateApiKeyRequest> selector = null,
			CancellationToken cancellationToken = default
		);

		/// <inheritdoc cref="InvalidateApiKey(System.Func{InvalidateApiKeyDescriptor,IInvalidateApiKeyRequest})" />
		Task<InvalidateApiKeyResponse> InvalidateApiKeyAsync(IInvalidateApiKeyRequest request,
			CancellationToken cancellationToken = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc cref="InvalidateApiKey(System.Func{InvalidateApiKeyDescriptor,IInvalidateApiKeyRequest})" />
		public InvalidateApiKeyResponse InvalidateApiKey(Func<InvalidateApiKeyDescriptor, IInvalidateApiKeyRequest> selector = null
		) =>
			InvalidateApiKey(selector.InvokeOrDefault(new InvalidateApiKeyDescriptor()));

		/// <inheritdoc cref="InvalidateApiKey(System.Func{InvalidateApiKeyDescriptor,IInvalidateApiKeyRequest})" />
		public InvalidateApiKeyResponse InvalidateApiKey(IInvalidateApiKeyRequest request) =>
			DoRequest<IInvalidateApiKeyRequest, InvalidateApiKeyResponse>(request, request.RequestParameters);

		/// <inheritdoc cref="InvalidateApiKey(System.Func{InvalidateApiKeyDescriptor,IInvalidateApiKeyRequest})" />
		public Task<InvalidateApiKeyResponse> InvalidateApiKeyAsync(
			Func<InvalidateApiKeyDescriptor, IInvalidateApiKeyRequest> selector = null,
			CancellationToken cancellationToken = default
		) =>
			InvalidateApiKeyAsync(selector.InvokeOrDefault(new InvalidateApiKeyDescriptor()), cancellationToken);

		/// <inheritdoc cref="InvalidateApiKey(System.Func{InvalidateApiKeyDescriptor,IInvalidateApiKeyRequest})" />
		public Task<InvalidateApiKeyResponse> InvalidateApiKeyAsync(IInvalidateApiKeyRequest request,
			CancellationToken cancellationToken = default
		) =>
			DoRequestAsync<IInvalidateApiKeyRequest, InvalidateApiKeyResponse>(request, request.RequestParameters, cancellationToken);
	}
}
