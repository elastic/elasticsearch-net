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
		ISecurityInvalidateApiKeyResponse SecurityInvalidateApiKey(Func<SecurityInvalidateApiKeyDescriptor, ISecurityInvalidateApiKeyRequest> selector = null);

		/// <inheritdoc cref="SecurityInvalidateApiKey(System.Func{Nest.SecurityInvalidateApiKeyDescriptor,Nest.ISecurityInvalidateApiKeyRequest})" />
		ISecurityInvalidateApiKeyResponse SecurityInvalidateApiKey(ISecurityInvalidateApiKeyRequest request);

		/// <inheritdoc cref="SecurityInvalidateApiKey(System.Func{Nest.SecurityInvalidateApiKeyDescriptor,Nest.ISecurityInvalidateApiKeyRequest})" />
		Task<ISecurityInvalidateApiKeyResponse> SecurityInvalidateApiKeyAsync(
			Func<SecurityInvalidateApiKeyDescriptor, ISecurityInvalidateApiKeyRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc cref="SecurityInvalidateApiKey(System.Func{Nest.SecurityInvalidateApiKeyDescriptor,Nest.ISecurityInvalidateApiKeyRequest})" />
		Task<ISecurityInvalidateApiKeyResponse> SecurityInvalidateApiKeyAsync(ISecurityInvalidateApiKeyRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc cref="SecurityInvalidateApiKey(System.Func{Nest.SecurityInvalidateApiKeyDescriptor,Nest.ISecurityInvalidateApiKeyRequest})" />
		public ISecurityInvalidateApiKeyResponse SecurityInvalidateApiKey(Func<SecurityInvalidateApiKeyDescriptor, ISecurityInvalidateApiKeyRequest> selector = null
		) =>
			SecurityInvalidateApiKey(selector.InvokeOrDefault(new SecurityInvalidateApiKeyDescriptor()));

		/// <inheritdoc cref="SecurityInvalidateApiKey(System.Func{Nest.SecurityInvalidateApiKeyDescriptor,Nest.ISecurityInvalidateApiKeyRequest})" />
		public ISecurityInvalidateApiKeyResponse SecurityInvalidateApiKey(ISecurityInvalidateApiKeyRequest request) =>
			Dispatcher.Dispatch<ISecurityInvalidateApiKeyRequest, SecurityInvalidateApiKeyRequestParameters, SecurityInvalidateApiKeyResponse>(
				request,
				LowLevelDispatch.SecurityInvalidateApiKeyDispatch<SecurityInvalidateApiKeyResponse>
			);

		/// <inheritdoc cref="SecurityInvalidateApiKey(System.Func{Nest.SecurityInvalidateApiKeyDescriptor,Nest.ISecurityInvalidateApiKeyRequest})" />
		public Task<ISecurityInvalidateApiKeyResponse> SecurityInvalidateApiKeyAsync(
			Func<SecurityInvalidateApiKeyDescriptor, ISecurityInvalidateApiKeyRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			SecurityInvalidateApiKeyAsync(selector.InvokeOrDefault(new SecurityInvalidateApiKeyDescriptor()), cancellationToken);
		/// <inheritdoc cref="SecurityInvalidateApiKey(System.Func{Nest.SecurityInvalidateApiKeyDescriptor,Nest.ISecurityInvalidateApiKeyRequest})" />
		public Task<ISecurityInvalidateApiKeyResponse> SecurityInvalidateApiKeyAsync(ISecurityInvalidateApiKeyRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			Dispatcher
				.DispatchAsync<ISecurityInvalidateApiKeyRequest, SecurityInvalidateApiKeyRequestParameters, SecurityInvalidateApiKeyResponse, ISecurityInvalidateApiKeyResponse>(
					request,
					cancellationToken,
					LowLevelDispatch.SecurityInvalidateApiKeyDispatchAsync<SecurityInvalidateApiKeyResponse>
				);
	}
}
