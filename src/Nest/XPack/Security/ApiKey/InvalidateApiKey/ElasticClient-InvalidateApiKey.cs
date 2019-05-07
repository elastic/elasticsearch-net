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
		IInvalidateApiKeyResponse InvalidateApiKey(Func<InvalidateApiKeyDescriptor, IInvalidateApiKeyRequest> selector = null);

		/// <inheritdoc cref="InvalidateApiKey(System.Func{InvalidateApiKeyDescriptor,IInvalidateApiKeyRequest})" />
		IInvalidateApiKeyResponse InvalidateApiKey(IInvalidateApiKeyRequest request);

		/// <inheritdoc cref="InvalidateApiKey(System.Func{InvalidateApiKeyDescriptor,IInvalidateApiKeyRequest})" />
		Task<IInvalidateApiKeyResponse> InvalidateApiKeyAsync(
			Func<InvalidateApiKeyDescriptor, IInvalidateApiKeyRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc cref="InvalidateApiKey(System.Func{InvalidateApiKeyDescriptor,IInvalidateApiKeyRequest})" />
		Task<IInvalidateApiKeyResponse> InvalidateApiKeyAsync(IInvalidateApiKeyRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc cref="InvalidateApiKey(System.Func{InvalidateApiKeyDescriptor,IInvalidateApiKeyRequest})" />
		public IInvalidateApiKeyResponse InvalidateApiKey(Func<InvalidateApiKeyDescriptor, IInvalidateApiKeyRequest> selector = null
		) =>
			InvalidateApiKey(selector.InvokeOrDefault(new InvalidateApiKeyDescriptor()));

		/// <inheritdoc cref="InvalidateApiKey(System.Func{InvalidateApiKeyDescriptor,IInvalidateApiKeyRequest})" />
		public IInvalidateApiKeyResponse InvalidateApiKey(IInvalidateApiKeyRequest request) =>
			Dispatcher.Dispatch<IInvalidateApiKeyRequest, InvalidateApiKeyRequestParameters, InvalidateApiKeyResponse>(
				request,
				LowLevelDispatch.XpackSecurityInvalidateApiKeyDispatch<InvalidateApiKeyResponse>
			);

		/// <inheritdoc cref="InvalidateApiKey(System.Func{InvalidateApiKeyDescriptor,IInvalidateApiKeyRequest})" />
		public Task<IInvalidateApiKeyResponse> InvalidateApiKeyAsync(
			Func<InvalidateApiKeyDescriptor, IInvalidateApiKeyRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			InvalidateApiKeyAsync(selector.InvokeOrDefault(new InvalidateApiKeyDescriptor()), cancellationToken);

		/// <inheritdoc cref="InvalidateApiKey(System.Func{InvalidateApiKeyDescriptor,IInvalidateApiKeyRequest})" />
		public Task<IInvalidateApiKeyResponse> InvalidateApiKeyAsync(IInvalidateApiKeyRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			Dispatcher
				.DispatchAsync<IInvalidateApiKeyRequest, InvalidateApiKeyRequestParameters, InvalidateApiKeyResponse, IInvalidateApiKeyResponse>(
					request,
					cancellationToken,
					LowLevelDispatch.XpackSecurityInvalidateApiKeyDispatchAsync<InvalidateApiKeyResponse>
				);
	}
}
