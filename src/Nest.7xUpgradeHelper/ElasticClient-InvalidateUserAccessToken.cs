using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static InvalidateUserAccessTokenResponse InvalidateUserAccessToken(this IElasticClient client, string token,
			Func<InvalidateUserAccessTokenDescriptor, IInvalidateUserAccessTokenRequest> selector = null
		)
			=> client.Security.InvalidateUserAccessToken(token, selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static InvalidateUserAccessTokenResponse InvalidateUserAccessToken(this IElasticClient client,
			IInvalidateUserAccessTokenRequest request
		)
			=> client.Security.InvalidateUserAccessToken(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<InvalidateUserAccessTokenResponse> InvalidateUserAccessTokenAsync(this IElasticClient client, string token,
			Func<InvalidateUserAccessTokenDescriptor, IInvalidateUserAccessTokenRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Security.InvalidateUserAccessTokenAsync(token, selector, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<InvalidateUserAccessTokenResponse> InvalidateUserAccessTokenAsync(this IElasticClient client,
			IInvalidateUserAccessTokenRequest request,
			CancellationToken ct = default
		)
			=> client.Security.InvalidateUserAccessTokenAsync(request, ct);
	}
}
