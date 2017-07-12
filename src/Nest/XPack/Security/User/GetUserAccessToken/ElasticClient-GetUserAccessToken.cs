using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using System.Threading;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		IGetUserAccessTokenResponse GetUserAccessToken(string username, string password, Func<GetUserAccessTokenDescriptor, IGetUserAccessTokenRequest> selector = null);

		/// <inheritdoc/>
		IGetUserAccessTokenResponse GetUserAccessToken(IGetUserAccessTokenRequest request);

		/// <inheritdoc/>
		Task<IGetUserAccessTokenResponse> GetUserAccessTokenAsync(string username, string password, Func<GetUserAccessTokenDescriptor, IGetUserAccessTokenRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken));

		/// <inheritdoc/>
		Task<IGetUserAccessTokenResponse> GetUserAccessTokenAsync(IGetUserAccessTokenRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IGetUserAccessTokenResponse GetUserAccessToken(string username, string password, Func<GetUserAccessTokenDescriptor, IGetUserAccessTokenRequest> selector = null) =>
			this.GetUserAccessToken(selector.InvokeOrDefault(new GetUserAccessTokenDescriptor(username, password)));

		/// <inheritdoc/>
		public IGetUserAccessTokenResponse GetUserAccessToken(IGetUserAccessTokenRequest request) =>
			this.Dispatcher.Dispatch<IGetUserAccessTokenRequest, GetUserAccessTokenRequestParameters, GetUserAccessTokenResponse>(
				request,
				(p, d) =>this.LowLevelDispatch.XpackSecurityGetTokenDispatch<GetUserAccessTokenResponse>(p, d)
			);

		/// <inheritdoc/>
		public Task<IGetUserAccessTokenResponse> GetUserAccessTokenAsync(string username, string password, Func<GetUserAccessTokenDescriptor, IGetUserAccessTokenRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.GetUserAccessTokenAsync(selector.InvokeOrDefault(new GetUserAccessTokenDescriptor(username, password)), cancellationToken);

		/// <inheritdoc/>
		public Task<IGetUserAccessTokenResponse> GetUserAccessTokenAsync(IGetUserAccessTokenRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<IGetUserAccessTokenRequest, GetUserAccessTokenRequestParameters, GetUserAccessTokenResponse, IGetUserAccessTokenResponse>(
				request,
				cancellationToken,
				(p, d, c) => this.LowLevelDispatch.XpackSecurityGetTokenDispatchAsync<GetUserAccessTokenResponse>(p, d, c)
			);
	}
}
