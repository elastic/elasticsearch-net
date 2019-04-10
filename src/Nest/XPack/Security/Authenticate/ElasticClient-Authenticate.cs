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
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<IAuthenticateResponse> AuthenticateAsync(IAuthenticateRequest request, CancellationToken ct = default);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc />
		public IAuthenticateResponse Authenticate(Func<AuthenticateDescriptor, IAuthenticateRequest> selector = null) =>
			Authenticate(selector.InvokeOrDefault(new AuthenticateDescriptor()));

		/// <inheritdoc />
		public IAuthenticateResponse Authenticate(IAuthenticateRequest request) =>
			DoRequest<IAuthenticateRequest, AuthenticateResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<IAuthenticateResponse> AuthenticateAsync(
			Func<AuthenticateDescriptor, IAuthenticateRequest> selector = null,
			CancellationToken ct = default
		) => AuthenticateAsync(selector.InvokeOrDefault(new AuthenticateDescriptor()), ct);

		/// <inheritdoc />
		public Task<IAuthenticateResponse> AuthenticateAsync(IAuthenticateRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IAuthenticateRequest, IAuthenticateResponse, AuthenticateResponse>(request, request.RequestParameters, ct);
	}
}
