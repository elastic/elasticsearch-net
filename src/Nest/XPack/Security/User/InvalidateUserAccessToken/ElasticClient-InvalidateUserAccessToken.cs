using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		IInvalidateUserAccessTokenResponse InvalidateUserAccessToken(string token,
			Func<InvalidateUserAccessTokenDescriptor, IInvalidateUserAccessTokenRequest> selector = null
		);

		/// <inheritdoc />
		IInvalidateUserAccessTokenResponse InvalidateUserAccessToken(IInvalidateUserAccessTokenRequest request);

		/// <inheritdoc />
		Task<IInvalidateUserAccessTokenResponse> InvalidateUserAccessTokenAsync(string token,
			Func<InvalidateUserAccessTokenDescriptor, IInvalidateUserAccessTokenRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<IInvalidateUserAccessTokenResponse> InvalidateUserAccessTokenAsync(IInvalidateUserAccessTokenRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IInvalidateUserAccessTokenResponse InvalidateUserAccessToken(string token,
			Func<InvalidateUserAccessTokenDescriptor, IInvalidateUserAccessTokenRequest> selector = null
		) =>
			InvalidateUserAccessToken(selector.InvokeOrDefault(new InvalidateUserAccessTokenDescriptor(token)));

		/// <inheritdoc />
		public IInvalidateUserAccessTokenResponse InvalidateUserAccessToken(IInvalidateUserAccessTokenRequest request) =>
			Dispatcher.Dispatch<IInvalidateUserAccessTokenRequest, InvalidateUserAccessTokenRequestParameters, InvalidateUserAccessTokenResponse>(
				request,
				(p, d) => LowLevelDispatch.SecurityInvalidateTokenDispatch<InvalidateUserAccessTokenResponse>(p, d)
			);

		/// <inheritdoc />
		public Task<IInvalidateUserAccessTokenResponse> InvalidateUserAccessTokenAsync(string token,
			Func<InvalidateUserAccessTokenDescriptor, IInvalidateUserAccessTokenRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			InvalidateUserAccessTokenAsync(selector.InvokeOrDefault(new InvalidateUserAccessTokenDescriptor(token)), cancellationToken);

		/// <inheritdoc />
		public Task<IInvalidateUserAccessTokenResponse> InvalidateUserAccessTokenAsync(IInvalidateUserAccessTokenRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			Dispatcher
				.DispatchAsync<IInvalidateUserAccessTokenRequest, InvalidateUserAccessTokenRequestParameters, InvalidateUserAccessTokenResponse,
					IInvalidateUserAccessTokenResponse>(
					request,
					cancellationToken,
					(p, d, c) => LowLevelDispatch.SecurityInvalidateTokenDispatchAsync<InvalidateUserAccessTokenResponse>(p, d, c)
				);
	}
}
