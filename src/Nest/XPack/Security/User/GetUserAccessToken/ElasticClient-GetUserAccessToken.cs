using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		IGetUserAccessTokenResponse GetUserAccessToken(string username, string password,
			Func<GetUserAccessTokenDescriptor, IGetUserAccessTokenRequest> selector = null
		);

		/// <inheritdoc />
		IGetUserAccessTokenResponse GetUserAccessToken(IGetUserAccessTokenRequest request);

		/// <inheritdoc />
		Task<IGetUserAccessTokenResponse> GetUserAccessTokenAsync(string username, string password,
			Func<GetUserAccessTokenDescriptor, IGetUserAccessTokenRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<IGetUserAccessTokenResponse> GetUserAccessTokenAsync(IGetUserAccessTokenRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IGetUserAccessTokenResponse GetUserAccessToken(string username, string password,
			Func<GetUserAccessTokenDescriptor, IGetUserAccessTokenRequest> selector = null
		) =>
			GetUserAccessToken(selector.InvokeOrDefault(new GetUserAccessTokenDescriptor(username, password)));

		/// <inheritdoc />
		public IGetUserAccessTokenResponse GetUserAccessToken(IGetUserAccessTokenRequest request) =>
			Dispatcher.Dispatch<IGetUserAccessTokenRequest, GetUserAccessTokenRequestParameters, GetUserAccessTokenResponse>(
				request,
				(p, d) => LowLevelDispatch.SecurityGetTokenDispatch<GetUserAccessTokenResponse>(p, d)
			);

		/// <inheritdoc />
		public Task<IGetUserAccessTokenResponse> GetUserAccessTokenAsync(string username, string password,
			Func<GetUserAccessTokenDescriptor, IGetUserAccessTokenRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			GetUserAccessTokenAsync(selector.InvokeOrDefault(new GetUserAccessTokenDescriptor(username, password)), cancellationToken);

		/// <inheritdoc />
		public Task<IGetUserAccessTokenResponse> GetUserAccessTokenAsync(IGetUserAccessTokenRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			Dispatcher
				.DispatchAsync<IGetUserAccessTokenRequest, GetUserAccessTokenRequestParameters, GetUserAccessTokenResponse,
					IGetUserAccessTokenResponse>(
					request,
					cancellationToken,
					(p, d, c) => LowLevelDispatch.SecurityGetTokenDispatchAsync<GetUserAccessTokenResponse>(p, d, c)
				);
	}
}
