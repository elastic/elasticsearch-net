using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public static partial class ElasticClientExtensions
	{
		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static GetUserAccessTokenResponse GetUserAccessToken(this IElasticClient client, string username, string password,
			Func<GetUserAccessTokenDescriptor, IGetUserAccessTokenRequest> selector = null
		)
			=> client.Security.GetUserAccessToken(username, password, selector);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static GetUserAccessTokenResponse GetUserAccessToken(this IElasticClient client, IGetUserAccessTokenRequest request)
			=> client.Security.GetUserAccessToken(request);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<GetUserAccessTokenResponse> GetUserAccessTokenAsync(this IElasticClient client, string username, string password,
			Func<GetUserAccessTokenDescriptor, IGetUserAccessTokenRequest> selector = null,
			CancellationToken ct = default
		)
			=> client.Security.GetUserAccessTokenAsync(username, password, selector, ct);

		/// <inheritdoc />
		[Obsolete("Moved to client.XX.XX(), please update this usage.")]
public static Task<GetUserAccessTokenResponse> GetUserAccessTokenAsync(this IElasticClient client, IGetUserAccessTokenRequest request,
			CancellationToken ct = default
		)
			=> client.Security.GetUserAccessTokenAsync(request, ct);
	}
}
