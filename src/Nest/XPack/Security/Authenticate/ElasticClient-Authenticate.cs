using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		IAuthenticateResponse Authenticate(Func<AuthenticateDescriptor, IAuthenticateRequest> selector = null);

		/// <inheritdoc />
		IAuthenticateResponse Authenticate(IAuthenticateRequest request);

		/// <inheritdoc />
		Task<IAuthenticateResponse> AuthenticateAsync(Func<AuthenticateDescriptor, IAuthenticateRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		);

		/// <inheritdoc />
		Task<IAuthenticateResponse> AuthenticateAsync(IAuthenticateRequest request, CancellationToken cancellationToken = default(CancellationToken));
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IAuthenticateResponse Authenticate(Func<AuthenticateDescriptor, IAuthenticateRequest> selector = null) =>
			Authenticate(selector.InvokeOrDefault(new AuthenticateDescriptor()));

		/// <inheritdoc />
		public IAuthenticateResponse Authenticate(IAuthenticateRequest request) =>
			Dispatcher.Dispatch<IAuthenticateRequest, AuthenticateRequestParameters, AuthenticateResponse>(
				request,
				(p, d) => LowLevelDispatch.SecurityAuthenticateDispatch<AuthenticateResponse>(p)
			);

		/// <inheritdoc />
		public Task<IAuthenticateResponse> AuthenticateAsync(Func<AuthenticateDescriptor, IAuthenticateRequest> selector = null,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			AuthenticateAsync(selector.InvokeOrDefault(new AuthenticateDescriptor()), cancellationToken);

		/// <inheritdoc />
		public Task<IAuthenticateResponse> AuthenticateAsync(IAuthenticateRequest request,
			CancellationToken cancellationToken = default(CancellationToken)
		) =>
			Dispatcher.DispatchAsync<IAuthenticateRequest, AuthenticateRequestParameters, AuthenticateResponse, IAuthenticateResponse>(
				request,
				cancellationToken,
				(p, d, c) => LowLevelDispatch.SecurityAuthenticateDispatchAsync<AuthenticateResponse>(p, c)
			);
	}
}
