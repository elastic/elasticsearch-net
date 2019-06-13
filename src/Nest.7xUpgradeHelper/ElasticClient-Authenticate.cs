using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		public static AuthenticateResponse Authenticate(this IElasticClient client,Func<AuthenticateDescriptor, IAuthenticateRequest> selector = null);

		/// <inheritdoc />
		public static AuthenticateResponse Authenticate(this IElasticClient client,IAuthenticateRequest request);

		/// <inheritdoc />
		public static Task<AuthenticateResponse> AuthenticateAsync(this IElasticClient client,Func<AuthenticateDescriptor, IAuthenticateRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		public static Task<AuthenticateResponse> AuthenticateAsync(this IElasticClient client,IAuthenticateRequest request, CancellationToken ct = default);
	}
}
