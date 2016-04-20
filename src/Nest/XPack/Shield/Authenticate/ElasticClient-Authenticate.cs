using System;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		IAuthenticateResponse Authenticate(Func<AuthenticateDescriptor, IAuthenticateRequest> selector = null);

		/// <inheritdoc/>
		IAuthenticateResponse Authenticate(IAuthenticateRequest request);

		/// <inheritdoc/>
		Task<IAuthenticateResponse> AuthenticateAsync(Func<AuthenticateDescriptor, IAuthenticateRequest> selector = null);

		/// <inheritdoc/>
		Task<IAuthenticateResponse> AuthenticateAsync(IAuthenticateRequest request);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc/>
		public IAuthenticateResponse Authenticate(Func<AuthenticateDescriptor, IAuthenticateRequest> selector = null) =>
			this.Authenticate(selector.InvokeOrDefault(new AuthenticateDescriptor()));

		/// <inheritdoc/>
		public IAuthenticateResponse Authenticate(IAuthenticateRequest request) =>
			this.Dispatcher.Dispatch<IAuthenticateRequest, AuthenticateRequestParameters, AuthenticateResponse>(
				request,
				(p, d) =>this.LowLevelDispatch.ShieldAuthenticateDispatch<AuthenticateResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IAuthenticateResponse> AuthenticateAsync(Func<AuthenticateDescriptor, IAuthenticateRequest> selector = null) =>
			this.AuthenticateAsync(selector.InvokeOrDefault(new AuthenticateDescriptor()));

		/// <inheritdoc/>
		public Task<IAuthenticateResponse> AuthenticateAsync(IAuthenticateRequest request) =>
			this.Dispatcher.DispatchAsync<IAuthenticateRequest, AuthenticateRequestParameters, AuthenticateResponse, IAuthenticateResponse>(
				request,
				(p,d ) => this.LowLevelDispatch.ShieldAuthenticateDispatchAsync<AuthenticateResponse>(p)
			);
	}
}
