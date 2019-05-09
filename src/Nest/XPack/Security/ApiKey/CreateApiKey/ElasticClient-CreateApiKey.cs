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
		CreateApiKeyResponse CreateApiKey(Func<CreateApiKeyDescriptor, ICreateApiKeyRequest> selector = null);

		/// <inheritdoc cref="CreateApiKey(System.Func{Nest.CreateApiKeyDescriptor,Nest.ICreateApiKeyRequest})" />
		CreateApiKeyResponse CreateApiKey(ICreateApiKeyRequest request);

		/// <inheritdoc cref="CreateApiKey(System.Func{Nest.CreateApiKeyDescriptor,Nest.ICreateApiKeyRequest})" />
		Task<CreateApiKeyResponse> CreateApiKeyAsync(
			Func<CreateApiKeyDescriptor, ICreateApiKeyRequest> selector = null,
			CancellationToken cancellationToken = default
		);

		/// <inheritdoc cref="CreateApiKey(System.Func{Nest.CreateApiKeyDescriptor,Nest.ICreateApiKeyRequest})" />
		Task<CreateApiKeyResponse> CreateApiKeyAsync(ICreateApiKeyRequest request,
			CancellationToken cancellationToken = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc cref="CreateApiKey(System.Func{Nest.CreateApiKeyDescriptor,Nest.ICreateApiKeyRequest})" />
		public CreateApiKeyResponse CreateApiKey(Func<CreateApiKeyDescriptor, ICreateApiKeyRequest> selector = null
		) => CreateApiKey(selector.InvokeOrDefault(new CreateApiKeyDescriptor()));

		/// <inheritdoc cref="CreateApiKey(System.Func{Nest.CreateApiKeyDescriptor,Nest.ICreateApiKeyRequest})" />
		public CreateApiKeyResponse CreateApiKey(ICreateApiKeyRequest request) =>
			DoRequest<ICreateApiKeyRequest, CreateApiKeyResponse>(request, request.RequestParameters);

		/// <inheritdoc cref="CreateApiKey(System.Func{Nest.CreateApiKeyDescriptor,Nest.ICreateApiKeyRequest})" />
		public Task<CreateApiKeyResponse> CreateApiKeyAsync(
			Func<CreateApiKeyDescriptor, ICreateApiKeyRequest> selector = null,
			CancellationToken cancellationToken = default
		) => CreateApiKeyAsync(selector.InvokeOrDefault(new CreateApiKeyDescriptor()), cancellationToken);

		/// <inheritdoc cref="CreateApiKey(System.Func{Nest.CreateApiKeyDescriptor,Nest.ICreateApiKeyRequest})" />
		public Task<CreateApiKeyResponse> CreateApiKeyAsync(ICreateApiKeyRequest request,
			CancellationToken cancellationToken = default
		) => DoRequestAsync<ICreateApiKeyRequest, CreateApiKeyResponse>(request, request.RequestParameters, cancellationToken);
	}
}
