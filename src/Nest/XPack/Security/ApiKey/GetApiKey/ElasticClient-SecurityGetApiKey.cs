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
		ISecurityGetApiKeyResponse SecurityGetApiKey(Func<SecurityGetApiKeyDescriptor, ISecurityGetApiKeyRequest> selector = null);

		/// <inheritdoc cref="SecurityGetApiKey(System.Func{Nest.SecurityGetApiKeyDescriptor,Nest.ISecurityGetApiKeyRequest})" />
		ISecurityGetApiKeyResponse SecurityGetApiKey(ISecurityGetApiKeyRequest request);

		/// <inheritdoc cref="SecurityGetApiKey(System.Func{Nest.SecurityGetApiKeyDescriptor,Nest.ISecurityGetApiKeyRequest})" />
		Task<ISecurityGetApiKeyResponse> SecurityGetApiKeyAsync(
			Func<SecurityGetApiKeyDescriptor, ISecurityGetApiKeyRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc cref="SecurityGetApiKey(System.Func{Nest.SecurityGetApiKeyDescriptor,Nest.ISecurityGetApiKeyRequest})" />
		Task<ISecurityGetApiKeyResponse> SecurityGetApiKeyAsync(ISecurityGetApiKeyRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc cref="SecurityGetApiKey(System.Func{Nest.SecurityGetApiKeyDescriptor,Nest.ISecurityGetApiKeyRequest})" />
		public ISecurityGetApiKeyResponse SecurityGetApiKey(Func<SecurityGetApiKeyDescriptor, ISecurityGetApiKeyRequest> selector = null
		) =>
			SecurityGetApiKey(selector.InvokeOrDefault(new SecurityGetApiKeyDescriptor()));

		/// <inheritdoc cref="SecurityGetApiKey(System.Func{Nest.SecurityGetApiKeyDescriptor,Nest.ISecurityGetApiKeyRequest})" />
		public ISecurityGetApiKeyResponse SecurityGetApiKey(ISecurityGetApiKeyRequest request) =>
			Dispatcher.Dispatch<ISecurityGetApiKeyRequest, SecurityGetApiKeyRequestParameters, SecurityGetApiKeyResponse>(
				request,
				(p, d) => LowLevelDispatch.SecurityGetApiKeyDispatch<SecurityGetApiKeyResponse>(p)
			);

		/// <inheritdoc cref="SecurityGetApiKey(System.Func{Nest.SecurityGetApiKeyDescriptor,Nest.ISecurityGetApiKeyRequest})" />
		public Task<ISecurityGetApiKeyResponse> SecurityGetApiKeyAsync(
			Func<SecurityGetApiKeyDescriptor, ISecurityGetApiKeyRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			SecurityGetApiKeyAsync(selector.InvokeOrDefault(new SecurityGetApiKeyDescriptor()), cancellationToken);

		/// <inheritdoc cref="SecurityGetApiKey(System.Func{Nest.SecurityGetApiKeyDescriptor,Nest.ISecurityGetApiKeyRequest})" />
		public Task<ISecurityGetApiKeyResponse> SecurityGetApiKeyAsync(ISecurityGetApiKeyRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			Dispatcher
				.DispatchAsync<ISecurityGetApiKeyRequest, SecurityGetApiKeyRequestParameters, SecurityGetApiKeyResponse, ISecurityGetApiKeyResponse>(
					request,
					cancellationToken,
					(p, d, c) => LowLevelDispatch.SecurityGetApiKeyDispatchAsync<SecurityGetApiKeyResponse>(p, c)
				);
	}
}
