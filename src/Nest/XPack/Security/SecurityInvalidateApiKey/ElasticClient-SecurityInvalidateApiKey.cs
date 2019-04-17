using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		ISecurityInvalidateApiKeyResponse SecurityInvalidateApiKey(Func<SecurityInvalidateApiKeyDescriptor, ISecurityInvalidateApiKeyRequest> selector = null);

		/// <inheritdoc />
		ISecurityInvalidateApiKeyResponse SecurityInvalidateApiKey(ISecurityInvalidateApiKeyRequest request);

		/// <inheritdoc />
		Task<ISecurityInvalidateApiKeyResponse> SecurityInvalidateApiKeyAsync(
			Func<SecurityInvalidateApiKeyDescriptor, ISecurityInvalidateApiKeyRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<ISecurityInvalidateApiKeyResponse> SecurityInvalidateApiKeyAsync(ISecurityInvalidateApiKeyRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public ISecurityInvalidateApiKeyResponse SecurityInvalidateApiKey(Func<SecurityInvalidateApiKeyDescriptor, ISecurityInvalidateApiKeyRequest> selector = null
		) =>
			SecurityInvalidateApiKey(selector.InvokeOrDefault(new SecurityInvalidateApiKeyDescriptor()));

		/// <inheritdoc />
		public ISecurityInvalidateApiKeyResponse SecurityInvalidateApiKey(ISecurityInvalidateApiKeyRequest request) =>
			Dispatcher.Dispatch<ISecurityInvalidateApiKeyRequest, SecurityInvalidateApiKeyRequestParameters, SecurityInvalidateApiKeyResponse>(
				request,
				LowLevelDispatch.SecurityInvalidateApiKeyDispatch<SecurityInvalidateApiKeyResponse>
			);

		/// <inheritdoc />
		public Task<ISecurityInvalidateApiKeyResponse> SecurityInvalidateApiKeyAsync(
			Func<SecurityInvalidateApiKeyDescriptor, ISecurityInvalidateApiKeyRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			SecurityInvalidateApiKeyAsync(selector.InvokeOrDefault(new SecurityInvalidateApiKeyDescriptor()), cancellationToken);

		/// <inheritdoc />
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
