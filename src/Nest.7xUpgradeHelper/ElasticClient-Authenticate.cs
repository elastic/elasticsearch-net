using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Security.Authenticate(), please update this usage.")]
		public static AuthenticateResponse Authenticate(this IElasticClient client, Func<AuthenticateDescriptor, IAuthenticateRequest> selector = null
		)
			=> client.Security.Authenticate(selector);

		[Obsolete("Moved to client.Security.Authenticate(), please update this usage.")]
		public static AuthenticateResponse Authenticate(this IElasticClient client, IAuthenticateRequest request)
			=> client.Security.Authenticate(request);

		[Obsolete("Moved to client.Security.AuthenticateAsync(), please update this usage.")]
		public static Task<AuthenticateResponse> AuthenticateAsync(this IElasticClient client,
			Func<AuthenticateDescriptor, IAuthenticateRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Security.AuthenticateAsync(selector, ct);

		[Obsolete("Moved to client.Security.AuthenticateAsync(), please update this usage.")]
		public static Task<AuthenticateResponse> AuthenticateAsync(this IElasticClient client, IAuthenticateRequest request,
			CancellationToken ct = default
		)
			=> client.Security.AuthenticateAsync(request, ct);
	}
}
