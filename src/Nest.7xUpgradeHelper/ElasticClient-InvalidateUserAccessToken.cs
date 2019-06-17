using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		[Obsolete("Moved to client.Security.InvalidateUserAccessToken(), please update this usage.")]
		public static InvalidateUserAccessTokenResponse InvalidateUserAccessToken(this IElasticClient client, string token,
			Func<InvalidateUserAccessTokenDescriptor, IInvalidateUserAccessTokenRequest> selector = null
		)
			=> client.Security.InvalidateUserAccessToken(token, selector);

		[Obsolete("Moved to client.Security.InvalidateUserAccessToken(), please update this usage.")]
		public static InvalidateUserAccessTokenResponse InvalidateUserAccessToken(this IElasticClient client,
			IInvalidateUserAccessTokenRequest request
		)
			=> client.Security.InvalidateUserAccessToken(request);

		[Obsolete("Moved to client.Security.InvalidateUserAccessTokenAsync(), please update this usage.")]
		public static Task<InvalidateUserAccessTokenResponse> InvalidateUserAccessTokenAsync(this IElasticClient client, string token,
			Func<InvalidateUserAccessTokenDescriptor, IInvalidateUserAccessTokenRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Security.InvalidateUserAccessTokenAsync(token, selector, ct);

		[Obsolete("Moved to client.Security.InvalidateUserAccessTokenAsync(), please update this usage.")]
		public static Task<InvalidateUserAccessTokenResponse> InvalidateUserAccessTokenAsync(this IElasticClient client,
			IInvalidateUserAccessTokenRequest request,
			CancellationToken ct = default
		)
			=> client.Security.InvalidateUserAccessTokenAsync(request, ct);
	}
}
