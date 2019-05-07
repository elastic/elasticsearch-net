using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// Creates an API key for access without requiring basic authentication.
		/// </summary>
		ICreateApiKeyResponse CreateApiKey(Func<CreateApiKeyDescriptor, ICreateApiKeyRequest> selector = null);

		/// <inheritdoc cref="CreateApiKey(System.Func{Nest.CreateApiKeyDescriptor,Nest.ICreateApiKeyRequest})" />
		ICreateApiKeyResponse CreateApiKey(ICreateApiKeyRequest request);

		/// <inheritdoc cref="CreateApiKey(System.Func{Nest.CreateApiKeyDescriptor,Nest.ICreateApiKeyRequest})" />
		Task<ICreateApiKeyResponse> CreateApiKeyAsync(
			Func<CreateApiKeyDescriptor, ICreateApiKeyRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc cref="CreateApiKey(System.Func{Nest.CreateApiKeyDescriptor,Nest.ICreateApiKeyRequest})" />
		Task<ICreateApiKeyResponse> CreateApiKeyAsync(ICreateApiKeyRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc cref="CreateApiKey(System.Func{Nest.CreateApiKeyDescriptor,Nest.ICreateApiKeyRequest})" />
		public ICreateApiKeyResponse CreateApiKey(Func<CreateApiKeyDescriptor, ICreateApiKeyRequest> selector = null
		) => CreateApiKey(selector.InvokeOrDefault(new CreateApiKeyDescriptor()));

		/// <inheritdoc cref="CreateApiKey(System.Func{Nest.CreateApiKeyDescriptor,Nest.ICreateApiKeyRequest})" />
		public ICreateApiKeyResponse CreateApiKey(ICreateApiKeyRequest request) =>
			Dispatcher.Dispatch<ICreateApiKeyRequest, CreateApiKeyRequestParameters, CreateApiKeyResponse>(
				request,
				LowLevelDispatch.XpackSecurityCreateApiKeyDispatch<CreateApiKeyResponse>
			);

		/// <inheritdoc cref="CreateApiKey(System.Func{Nest.CreateApiKeyDescriptor,Nest.ICreateApiKeyRequest})" />
		public Task<ICreateApiKeyResponse> CreateApiKeyAsync(
			Func<CreateApiKeyDescriptor, ICreateApiKeyRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) => CreateApiKeyAsync(selector.InvokeOrDefault(new CreateApiKeyDescriptor()), cancellationToken);

		/// <inheritdoc cref="CreateApiKey(System.Func{Nest.CreateApiKeyDescriptor,Nest.ICreateApiKeyRequest})" />
		public Task<ICreateApiKeyResponse> CreateApiKeyAsync(ICreateApiKeyRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) => Dispatcher
				.DispatchAsync<ICreateApiKeyRequest, CreateApiKeyRequestParameters, CreateApiKeyResponse, ICreateApiKeyResponse>(
					request,
					cancellationToken,
					LowLevelDispatch.XpackSecurityCreateApiKeyDispatchAsync<CreateApiKeyResponse>
				);
	}
}
