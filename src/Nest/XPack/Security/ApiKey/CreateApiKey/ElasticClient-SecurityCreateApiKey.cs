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
		ISecurityCreateApiKeyResponse SecurityCreateApiKey(Func<SecurityCreateApiKeyDescriptor, ISecurityCreateApiKeyRequest> selector = null);

		/// <inheritdoc cref="SecurityCreateApiKey(System.Func{Nest.SecurityCreateApiKeyDescriptor,Nest.ISecurityCreateApiKeyRequest})" />
		ISecurityCreateApiKeyResponse SecurityCreateApiKey(ISecurityCreateApiKeyRequest request);

		/// <inheritdoc cref="SecurityCreateApiKey(System.Func{Nest.SecurityCreateApiKeyDescriptor,Nest.ISecurityCreateApiKeyRequest})" />
		Task<ISecurityCreateApiKeyResponse> SecurityCreateApiKeyAsync(
			Func<SecurityCreateApiKeyDescriptor, ISecurityCreateApiKeyRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc cref="SecurityCreateApiKey(System.Func{Nest.SecurityCreateApiKeyDescriptor,Nest.ISecurityCreateApiKeyRequest})" />
		Task<ISecurityCreateApiKeyResponse> SecurityCreateApiKeyAsync(ISecurityCreateApiKeyRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc cref="SecurityCreateApiKey(System.Func{Nest.SecurityCreateApiKeyDescriptor,Nest.ISecurityCreateApiKeyRequest})" />
		public ISecurityCreateApiKeyResponse SecurityCreateApiKey(Func<SecurityCreateApiKeyDescriptor, ISecurityCreateApiKeyRequest> selector = null
		) => SecurityCreateApiKey(selector.InvokeOrDefault(new SecurityCreateApiKeyDescriptor()));

		/// <inheritdoc cref="SecurityCreateApiKey(System.Func{Nest.SecurityCreateApiKeyDescriptor,Nest.ISecurityCreateApiKeyRequest})" />
		public ISecurityCreateApiKeyResponse SecurityCreateApiKey(ISecurityCreateApiKeyRequest request) =>
			Dispatcher.Dispatch<ISecurityCreateApiKeyRequest, SecurityCreateApiKeyRequestParameters, SecurityCreateApiKeyResponse>(
				request,
				LowLevelDispatch.SecurityCreateApiKeyDispatch<SecurityCreateApiKeyResponse>
			);

		/// <inheritdoc cref="SecurityCreateApiKey(System.Func{Nest.SecurityCreateApiKeyDescriptor,Nest.ISecurityCreateApiKeyRequest})" />
		public Task<ISecurityCreateApiKeyResponse> SecurityCreateApiKeyAsync(
			Func<SecurityCreateApiKeyDescriptor, ISecurityCreateApiKeyRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) => SecurityCreateApiKeyAsync(selector.InvokeOrDefault(new SecurityCreateApiKeyDescriptor()), cancellationToken);

		/// <inheritdoc cref="SecurityCreateApiKey(System.Func{Nest.SecurityCreateApiKeyDescriptor,Nest.ISecurityCreateApiKeyRequest})" />
		public Task<ISecurityCreateApiKeyResponse> SecurityCreateApiKeyAsync(ISecurityCreateApiKeyRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) => Dispatcher
				.DispatchAsync<ISecurityCreateApiKeyRequest, SecurityCreateApiKeyRequestParameters, SecurityCreateApiKeyResponse, ISecurityCreateApiKeyResponse>(
					request,
					cancellationToken,
					LowLevelDispatch.SecurityCreateApiKeyDispatchAsync<SecurityCreateApiKeyResponse>
				);
	}
}
