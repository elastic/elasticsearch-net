using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		AuthenticateResponse Authenticate(Func<AuthenticateDescriptor, IAuthenticateRequest> selector = null);

		/// <inheritdoc />
		AuthenticateResponse Authenticate(IAuthenticateRequest request);

		/// <inheritdoc />
		Task<AuthenticateResponse> AuthenticateAsync(Func<AuthenticateDescriptor, IAuthenticateRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<AuthenticateResponse> AuthenticateAsync(IAuthenticateRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public AuthenticateResponse Authenticate(Func<AuthenticateDescriptor, IAuthenticateRequest> selector = null) =>
			Authenticate(selector.InvokeOrDefault(new AuthenticateDescriptor()));

		/// <inheritdoc />
		public AuthenticateResponse Authenticate(IAuthenticateRequest request) =>
			DoRequest<IAuthenticateRequest, AuthenticateResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<AuthenticateResponse> AuthenticateAsync(
			Func<AuthenticateDescriptor, IAuthenticateRequest> selector = null,
			CancellationToken ct = default
		) => AuthenticateAsync(selector.InvokeOrDefault(new AuthenticateDescriptor()), ct);

		/// <inheritdoc />
		public Task<AuthenticateResponse> AuthenticateAsync(IAuthenticateRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IAuthenticateRequest, AuthenticateResponse>(request, request.RequestParameters, ct);
	}
}
