using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		GetUserAccessTokenResponse GetUserAccessToken(string username, string password,
			Func<GetUserAccessTokenDescriptor, IGetUserAccessTokenRequest> selector = null
		);

		/// <inheritdoc />
		GetUserAccessTokenResponse GetUserAccessToken(IGetUserAccessTokenRequest request);

		/// <inheritdoc />
		Task<GetUserAccessTokenResponse> GetUserAccessTokenAsync(string username, string password,
			Func<GetUserAccessTokenDescriptor, IGetUserAccessTokenRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<GetUserAccessTokenResponse> GetUserAccessTokenAsync(IGetUserAccessTokenRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public GetUserAccessTokenResponse GetUserAccessToken(
			string username,
			string password,
			Func<GetUserAccessTokenDescriptor, IGetUserAccessTokenRequest> selector = null
		) =>
			GetUserAccessToken(selector.InvokeOrDefault(new GetUserAccessTokenDescriptor(username, password)));

		/// <inheritdoc />
		public GetUserAccessTokenResponse GetUserAccessToken(IGetUserAccessTokenRequest request) =>
			DoRequest<IGetUserAccessTokenRequest, GetUserAccessTokenResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<GetUserAccessTokenResponse> GetUserAccessTokenAsync(
			string username,
			string password,
			Func<GetUserAccessTokenDescriptor, IGetUserAccessTokenRequest> selector = null,
			CancellationToken ct = default
		) => GetUserAccessTokenAsync(selector.InvokeOrDefault(new GetUserAccessTokenDescriptor(username, password)), ct);

		/// <inheritdoc />
		public Task<GetUserAccessTokenResponse> GetUserAccessTokenAsync(IGetUserAccessTokenRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IGetUserAccessTokenRequest, GetUserAccessTokenResponse>(request, request.RequestParameters, ct);
	}
}
