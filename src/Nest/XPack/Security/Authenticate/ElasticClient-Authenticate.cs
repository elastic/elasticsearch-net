using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using System.Threading;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc/>
		IAuthenticateResponse Authenticate(Func<AuthenticateDescriptor, IAuthenticateRequest> selector = null);

		/// <inheritdoc/>
		IAuthenticateResponse Authenticate(IAuthenticateRequest request);

		/// <inheritdoc/>
		Task<IAuthenticateResponse> AuthenticateAsync(Func<AuthenticateDescriptor, IAuthenticateRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken));

		/// <inheritdoc/>
		Task<IAuthenticateResponse> AuthenticateAsync(IAuthenticateRequest request, CancellationToken cancellationToken = default(CancellationToken));
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
				(p, d) =>this.LowLevelDispatch.XpackSecurityAuthenticateDispatch<AuthenticateResponse>(p)
			);

		/// <inheritdoc/>
		public Task<IAuthenticateResponse> AuthenticateAsync(Func<AuthenticateDescriptor, IAuthenticateRequest> selector = null, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.AuthenticateAsync(selector.InvokeOrDefault(new AuthenticateDescriptor()), cancellationToken);

		/// <inheritdoc/>
		public Task<IAuthenticateResponse> AuthenticateAsync(IAuthenticateRequest request, CancellationToken cancellationToken = default(CancellationToken)) =>
			this.Dispatcher.DispatchAsync<IAuthenticateRequest, AuthenticateRequestParameters, AuthenticateResponse, IAuthenticateResponse>(
				request,
				cancellationToken,
				(p, d, c) => this.LowLevelDispatch.XpackSecurityAuthenticateDispatchAsync<AuthenticateResponse>(p, c)
			);
	}
}
