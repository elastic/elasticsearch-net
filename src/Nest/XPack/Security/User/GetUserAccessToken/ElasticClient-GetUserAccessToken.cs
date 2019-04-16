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
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<IGetUserAccessTokenResponse> GetUserAccessTokenAsync(IGetUserAccessTokenRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IGetUserAccessTokenResponse GetUserAccessToken(
			string username,
			string password,
			Func<GetUserAccessTokenDescriptor, IGetUserAccessTokenRequest> selector = null
		) =>
			GetUserAccessToken(selector.InvokeOrDefault(new GetUserAccessTokenDescriptor(username, password)));

		/// <inheritdoc />
		public IGetUserAccessTokenResponse GetUserAccessToken(IGetUserAccessTokenRequest request) =>
			DoRequest<IGetUserAccessTokenRequest, GetUserAccessTokenResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IGetUserAccessTokenResponse> GetUserAccessTokenAsync(
			string username,
			string password,
			Func<GetUserAccessTokenDescriptor, IGetUserAccessTokenRequest> selector = null,
			CancellationToken ct = default
		) => GetUserAccessTokenAsync(selector.InvokeOrDefault(new GetUserAccessTokenDescriptor(username, password)), ct);

		/// <inheritdoc />
		public Task<IGetUserAccessTokenResponse> GetUserAccessTokenAsync(IGetUserAccessTokenRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IGetUserAccessTokenRequest, IGetUserAccessTokenResponse, GetUserAccessTokenResponse>(request, request.RequestParameters, ct);
	}
}
