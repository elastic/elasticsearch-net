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
		GetApiKeyResponse GetApiKey(Func<GetApiKeyDescriptor, IGetApiKeyRequest> selector = null);

		/// <inheritdoc cref="GetApiKey(System.Func{Nest.GetApiKeyDescriptor,Nest.IGetApiKeyRequest})" />
		GetApiKeyResponse GetApiKey(IGetApiKeyRequest request);

		/// <inheritdoc cref="GetApiKey(System.Func{Nest.GetApiKeyDescriptor,Nest.IGetApiKeyRequest})" />
		Task<GetApiKeyResponse> GetApiKeyAsync(
			Func<GetApiKeyDescriptor, IGetApiKeyRequest> selector = null,
			CancellationToken cancellationToken = default
		);

		/// <inheritdoc cref="GetApiKey(System.Func{Nest.GetApiKeyDescriptor,Nest.IGetApiKeyRequest})" />
		Task<GetApiKeyResponse> GetApiKeyAsync(IGetApiKeyRequest request,
			CancellationToken cancellationToken = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc cref="GetApiKey(System.Func{Nest.GetApiKeyDescriptor,Nest.IGetApiKeyRequest})" />
		public GetApiKeyResponse GetApiKey(Func<GetApiKeyDescriptor, IGetApiKeyRequest> selector = null
		) =>
			GetApiKey(selector.InvokeOrDefault(new GetApiKeyDescriptor()));

		/// <inheritdoc cref="GetApiKey(System.Func{Nest.GetApiKeyDescriptor,Nest.IGetApiKeyRequest})" />
		public GetApiKeyResponse GetApiKey(IGetApiKeyRequest request) =>
			DoRequest<IGetApiKeyRequest, GetApiKeyResponse>(request, request.RequestParameters);

		/// <inheritdoc cref="GetApiKey(System.Func{Nest.GetApiKeyDescriptor,Nest.IGetApiKeyRequest})" />
		public Task<GetApiKeyResponse> GetApiKeyAsync(
			Func<GetApiKeyDescriptor, IGetApiKeyRequest> selector = null,
			CancellationToken cancellationToken = default
		) =>
			GetApiKeyAsync(selector.InvokeOrDefault(new GetApiKeyDescriptor()), cancellationToken);

		/// <inheritdoc cref="GetApiKey(System.Func{Nest.GetApiKeyDescriptor,Nest.IGetApiKeyRequest})" />
		public Task<GetApiKeyResponse> GetApiKeyAsync(IGetApiKeyRequest request,
			CancellationToken cancellationToken = default
		) =>
			DoRequestAsync<IGetApiKeyRequest, GetApiKeyResponse>(request, request.RequestParameters, cancellationToken);
	}
}
