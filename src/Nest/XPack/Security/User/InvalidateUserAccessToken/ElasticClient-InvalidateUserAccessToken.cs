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
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<IInvalidateUserAccessTokenResponse> InvalidateUserAccessTokenAsync(IInvalidateUserAccessTokenRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IInvalidateUserAccessTokenResponse InvalidateUserAccessToken(
			string token,
			Func<InvalidateUserAccessTokenDescriptor, IInvalidateUserAccessTokenRequest> selector = null
		) => InvalidateUserAccessToken(selector.InvokeOrDefault(new InvalidateUserAccessTokenDescriptor(token)));

		/// <inheritdoc />
		public IInvalidateUserAccessTokenResponse InvalidateUserAccessToken(IInvalidateUserAccessTokenRequest request) =>
			DoRequest<IInvalidateUserAccessTokenRequest, InvalidateUserAccessTokenResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IInvalidateUserAccessTokenResponse> InvalidateUserAccessTokenAsync(
			string token,
			Func<InvalidateUserAccessTokenDescriptor, IInvalidateUserAccessTokenRequest> selector = null,
			CancellationToken ct = default
		) => InvalidateUserAccessTokenAsync(selector.InvokeOrDefault(new InvalidateUserAccessTokenDescriptor(token)), ct);

		/// <inheritdoc />
		public Task<IInvalidateUserAccessTokenResponse> InvalidateUserAccessTokenAsync(
			IInvalidateUserAccessTokenRequest request,
			CancellationToken ct = default
		) =>
			DoRequestAsync<IInvalidateUserAccessTokenRequest, IInvalidateUserAccessTokenResponse, InvalidateUserAccessTokenResponse>
				(request, request.RequestParameters, ct);
	}
}
