using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		InvalidateUserAccessTokenResponse InvalidateUserAccessToken(string token,
			Func<InvalidateUserAccessTokenDescriptor, IInvalidateUserAccessTokenRequest> selector = null
		);

		/// <inheritdoc />
		InvalidateUserAccessTokenResponse InvalidateUserAccessToken(IInvalidateUserAccessTokenRequest request);

		/// <inheritdoc />
		Task<InvalidateUserAccessTokenResponse> InvalidateUserAccessTokenAsync(string token,
			Func<InvalidateUserAccessTokenDescriptor, IInvalidateUserAccessTokenRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<InvalidateUserAccessTokenResponse> InvalidateUserAccessTokenAsync(IInvalidateUserAccessTokenRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public InvalidateUserAccessTokenResponse InvalidateUserAccessToken(
			string token,
			Func<InvalidateUserAccessTokenDescriptor, IInvalidateUserAccessTokenRequest> selector = null
		) => InvalidateUserAccessToken(selector.InvokeOrDefault(new InvalidateUserAccessTokenDescriptor(token)));

		/// <inheritdoc />
		public InvalidateUserAccessTokenResponse InvalidateUserAccessToken(IInvalidateUserAccessTokenRequest request) =>
			DoRequest<IInvalidateUserAccessTokenRequest, InvalidateUserAccessTokenResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<InvalidateUserAccessTokenResponse> InvalidateUserAccessTokenAsync(
			string token,
			Func<InvalidateUserAccessTokenDescriptor, IInvalidateUserAccessTokenRequest> selector = null,
			CancellationToken ct = default
		) => InvalidateUserAccessTokenAsync(selector.InvokeOrDefault(new InvalidateUserAccessTokenDescriptor(token)), ct);

		/// <inheritdoc />
		public Task<InvalidateUserAccessTokenResponse> InvalidateUserAccessTokenAsync(
			IInvalidateUserAccessTokenRequest request,
			CancellationToken ct = default
		) =>
			DoRequestAsync<IInvalidateUserAccessTokenRequest, InvalidateUserAccessTokenResponse>
				(request, request.RequestParameters, ct);
	}
}
