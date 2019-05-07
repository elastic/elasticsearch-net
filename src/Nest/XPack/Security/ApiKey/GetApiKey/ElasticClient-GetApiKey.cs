using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Retrieves information for one or more API keys.
		/// </summary>
		IGetApiKeyResponse GetApiKey(Func<GetApiKeyDescriptor, IGetApiKeyRequest> selector = null);

		/// <inheritdoc cref="GetApiKey(System.Func{Nest.GetApiKeyDescriptor,Nest.IGetApiKeyRequest})" />
		IGetApiKeyResponse GetApiKey(IGetApiKeyRequest request);

		/// <inheritdoc cref="GetApiKey(System.Func{Nest.GetApiKeyDescriptor,Nest.IGetApiKeyRequest})" />
		Task<IGetApiKeyResponse> GetApiKeyAsync(
			Func<GetApiKeyDescriptor, IGetApiKeyRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc cref="GetApiKey(System.Func{Nest.GetApiKeyDescriptor,Nest.IGetApiKeyRequest})" />
		Task<IGetApiKeyResponse> GetApiKeyAsync(IGetApiKeyRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc cref="GetApiKey(System.Func{Nest.GetApiKeyDescriptor,Nest.IGetApiKeyRequest})" />
		public IGetApiKeyResponse GetApiKey(Func<GetApiKeyDescriptor, IGetApiKeyRequest> selector = null
		) =>
			GetApiKey(selector.InvokeOrDefault(new GetApiKeyDescriptor()));

		/// <inheritdoc cref="GetApiKey(System.Func{Nest.GetApiKeyDescriptor,Nest.IGetApiKeyRequest})" />
		public IGetApiKeyResponse GetApiKey(IGetApiKeyRequest request) =>
			Dispatcher.Dispatch<IGetApiKeyRequest, GetApiKeyRequestParameters, GetApiKeyResponse>(
				request,
				(p, d) => LowLevelDispatch.XpackSecurityGetApiKeyDispatch<GetApiKeyResponse>(p)
			);

		/// <inheritdoc cref="GetApiKey(System.Func{Nest.GetApiKeyDescriptor,Nest.IGetApiKeyRequest})" />
		public Task<IGetApiKeyResponse> GetApiKeyAsync(
			Func<GetApiKeyDescriptor, IGetApiKeyRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			GetApiKeyAsync(selector.InvokeOrDefault(new GetApiKeyDescriptor()), cancellationToken);

		/// <inheritdoc cref="GetApiKey(System.Func{Nest.GetApiKeyDescriptor,Nest.IGetApiKeyRequest})" />
		public Task<IGetApiKeyResponse> GetApiKeyAsync(IGetApiKeyRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			Dispatcher
				.DispatchAsync<IGetApiKeyRequest, GetApiKeyRequestParameters, GetApiKeyResponse, IGetApiKeyResponse>(
					request,
					cancellationToken,
					(p, d, c) => LowLevelDispatch.XpackSecurityGetApiKeyDispatchAsync<GetApiKeyResponse>(p, c)
				);
	}
}
